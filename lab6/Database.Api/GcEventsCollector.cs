using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Database.Api
{
    public class GcEventsCollector : IHostedService, IDisposable
    {
        private Timer _timer;
        private Meter _gen0Collections = new Meter("DatabaseAPI.Gen0Collections", "1.0");
        private Counter<int> _gen0CollectionsCounter;
        private Meter _gen1Collections = new Meter("DatabaseAPI.Gen1Collections", "1.0");
        private Counter<int> _gen1CollectionsCounter;
        private Meter _gen2Collections = new Meter("DatabaseAPI.Gen2Collections", "1.0");
        private Counter<int> _gen2CollectionsCounter;
        private Meter _totalMemory = new Meter("DatabaseAPI.TotalMemory");
        private Counter<long> _totalMemoryCounter;
        private GcEventListener _gcTest;


        public GcEventsCollector()
        {
            
        }



        public Task StartAsync(CancellationToken cancellationToken)
        {
            _gen0CollectionsCounter = _gen0Collections.CreateCounter<int>("database_api_gen_0_collections");
            _gen1CollectionsCounter = _gen1Collections.CreateCounter<int>("database_api_gen_1_collections");
            _gen2CollectionsCounter = _gen2Collections.CreateCounter<int>("database_api_gen_2_collections");
            _totalMemoryCounter = _totalMemory.CreateCounter<long>("database_api_total_memory");

            _timer = new Timer(CollectData, null, 0, 5000);
            _gcTest = new GcEventListener();
            return Task.CompletedTask;
        }

        private void CollectData(object state)
        {
            _gen0CollectionsCounter.Add(GC.CollectionCount(0));
            _gen1CollectionsCounter.Add(GC.CollectionCount(1));
            _gen2CollectionsCounter.Add(GC.CollectionCount(2));
            _totalMemoryCounter.Add(GC.GetTotalMemory(false));
        }

        sealed class GcEventListener : EventListener
        {
            private const int GC_KEYWORD = 0x0000001;

            private readonly Meter _allocatedMemory = new Meter("DatabaseAPI.AllocatedMemory", "1.0");
            private readonly Counter<long> _allocatedMemoryCounter;
            private readonly Meter _gen0Size = new Meter("DatabaseAPI.Gen0Size", "1.0");
            private readonly Counter<long> _gen0SizeCounter;
            private readonly Meter _gen1Size = new Meter("DatabaseAPI.Gen1Size", "1.0");
            private readonly Counter<long> _gen1SizeCounter;
            private readonly Meter _gen2Size = new Meter("DatabaseAPI.Gen2Size", "1.0");
            private readonly Counter<long> _gen2SizeCounter;

            private readonly Meter _lohSize = new Meter("DatabaseAPI.LoHSize", "1.0");
            private readonly Counter<long> _lohSizeCounter;

            private readonly Meter _gen0Promoted = new Meter("DatabaseAPI.Gen0Promoted", "1.0");
            private readonly Counter<long> _gen0PromotedCounter;

            private readonly Meter _gen1Promoted = new Meter("DatabaseAPI.Gen1Promoted", "1.0");
            private readonly Counter<long> _gen1PromotedCounter;

            private readonly Meter _gen2Survived = new Meter("DatabaseAPI.Gen2Survived", "1.0");
            private readonly Counter<long> _gen2SurvivedCounter;

            private readonly Meter _lohSurvived = new Meter("DatabaseAPI.LoHSurvived", "1.0");
            private readonly Counter<long> _lohSurvivedCounter;
            private EventSource _dotNetRuntime;

            public GcEventListener()
            {
                _gen0SizeCounter = _gen0Size.CreateCounter<long>("database_api_gen_0_heap_size");
                _gen1SizeCounter = _gen1Size.CreateCounter<long>("database_api_gen_1_heap_size");
                _gen2SizeCounter = _gen2Size.CreateCounter<long>("database_api_gen_2_heap_size");

                _lohSizeCounter = _lohSize.CreateCounter<long>("database_api_large_object_heap_size");

                _gen0PromotedCounter = _gen0Promoted.CreateCounter<long>("database_api_bytes_promoted_from_gen_0");
                _gen1PromotedCounter = _gen1Promoted.CreateCounter<long>("database_api_bytes_promoted_from_gen_1");
                _gen2SurvivedCounter = _gen2Survived.CreateCounter<long>("database_api_bytes_survived_gen_2");

                _allocatedMemoryCounter = _allocatedMemory.CreateCounter<long>("database_api_allocated_memory");
                _lohSurvivedCounter = _lohSurvived.CreateCounter<long>("database_api_bytes_survived_large_object_heap");
            }

            protected override void OnEventSourceCreated(EventSource eventSource)
            {
                if (eventSource.Name.Equals("Microsoft-Windows-DotNETRuntime"))
                {
                    _dotNetRuntime = eventSource;

                    EnableEvents(eventSource, EventLevel.Verbose, (EventKeywords)GC_KEYWORD);
                }
            }

            protected override void OnEventWritten(EventWrittenEventArgs eventData)
            {
                switch (eventData.EventName)
                {
                    case "GCHeapStats_V2":
                        ProcessHeapStats(eventData);
                        break;
                    case "GCAllocationTick_V3":
                        ProcessAllocationEvent(eventData);
                        break;
                }
            }

            private void ProcessAllocationEvent(EventWrittenEventArgs eventData)
            {
                _allocatedMemoryCounter.Add(MapULongToLong((ulong)eventData.Payload[3]));
            }

            private void ProcessHeapStats(EventWrittenEventArgs eventData)
            {
                _gen0SizeCounter.Add(MapULongToLong((ulong)eventData.Payload[0]));
                _gen0PromotedCounter.Add(MapULongToLong((ulong)eventData.Payload[1]));
                _gen1SizeCounter.Add(MapULongToLong((ulong)eventData.Payload[2]));
                _gen1PromotedCounter.Add(MapULongToLong((ulong)eventData.Payload[3]));
                _gen2SizeCounter.Add(MapULongToLong((ulong)eventData.Payload[4]));
                _gen2SurvivedCounter.Add(MapULongToLong((ulong)eventData.Payload[5]));
                _lohSizeCounter.Add(MapULongToLong((ulong)eventData.Payload[6]));
                _lohSurvivedCounter.Add(MapULongToLong((ulong)eventData.Payload[7]));
            }

            private static long MapULongToLong(ulong ulongValue)
            {
                return unchecked((long) ulongValue);
            }
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer.Dispose();
            _gcTest.Dispose();
        }
    }
}
