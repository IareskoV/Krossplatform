Ç
:E:\Labs\CrossPlatform\Lab6\IdentityServer\Configuration.cs
	namespace

 	
IdentityServer


 
{ 
public 

static 
class 
Configuration %
{ 
public 
static 
IEnumerable !
<! "
Client" (
>( )

GetClients* 4
(4 5
)5 6
=>7 9
new 
List 
< 
Client 
> 
{ 	
new 
Client 
{ 
ClientId 
= 
$str *
,* +
ClientSecrets 
= 
new  #
List$ (
<( )
Secret) /
>/ 0
{0 1
new1 4
Secret5 ;
(; <
$str< O
.O P
ToSha256P X
(X Y
)Y Z
)Z [
}[ \
,\ ]
AllowedGrantTypes !
=" #

GrantTypes$ .
.. /
Code/ 3
,3 4
AllowedScopes 
= 
{ 
$str !
,! "#
IdentityServerConstants +
.+ ,
StandardScopes, :
.: ;
OpenId; A
,A B#
IdentityServerConstants +
.+ ,
StandardScopes, :
.: ;
Profile; B
} 
, 
RedirectUris 
= 
{  
$str  C
}C D
,D E,
 AlwaysIncludeUserClaimsInIdToken 0
=1 2
true3 7
} 
} 	
;	 

public!! 
static!! 
IEnumerable!! !
<!!! "
ApiScope!!" *
>!!* +
GetApiScopes!!, 8
(!!8 9
)!!9 :
=>!!; =
new"" 
List"" 
<"" 
ApiScope"" 
>"" 
{## 	
new$$ 
ApiScope$$ 
($$ 
$str$$ &
)$$& '
}%% 	
;%%	 

public'' 
static'' 
IEnumerable'' !
<''! "
ApiResource''" -
>''- .
GetApiResources''/ >
(''> ?
)''? @
=>''A C
new(( 
List(( 
<(( 
ApiResource(( 
>(( 
{)) 	
new** 
ApiResource** 
{++ 
Name,, 
=,, 
$str,, $
,,,$ %
Scopes-- 
=-- 
new-- 
List-- !
<--! "
string--" (
>--( )
{.. 
$str// !
,//! "#
IdentityServerConstants00 +
.00+ ,
StandardScopes00, :
.00: ;
OpenId00; A
,00A B#
IdentityServerConstants11 +
.11+ ,
StandardScopes11, :
.11: ;
Profile11; B
}22 
}33 
}44 	
;44	 

public66 
static66 
IEnumerable66 !
<66! "
IdentityResource66" 2
>662 3 
GetIdentityResources664 H
(66H I
)66I J
=>66K M
new77 
List77 
<77 
IdentityResource77 !
>77! "
{88 	
new99 
IdentityResources99 !
.99! "
OpenId99" (
(99( )
)99) *
,99* +
new:: 
IdentityResources:: !
.::! "
Profile::" )
(::) *
)::* +
};; 	
;;;	 

}<< 
}== ΩE
GE:\Labs\CrossPlatform\Lab6\IdentityServer\Controllers\AuthController.cs
	namespace

 	
IdentityServer


 
.

 
Controllers

 $
{ 
public 

class 
AuthController 
:  !

Controller" ,
{ 
private 
readonly 
SignInManager &
<& '
AppUser' .
>. /
_signInManager0 >
;> ?
private 
readonly 
UserManager $
<$ %
AppUser% ,
>, -
_userManager. :
;: ;
private 
readonly -
!IIdentityServerInteractionService :
_interactionService; N
;N O
public 
AuthController 
( 
SignInManager +
<+ ,
AppUser, 3
>3 4
signInManager5 B
,B C
UserManagerD O
<O P
AppUserP W
>W X
userManagerY d
,d e.
!IIdentityServerInteractionService	f á 
interactionService
à ö
)
ö õ
{ 	
_signInManager 
= 
signInManager *
;* +
_userManager 
= 
userManager &
;& '
_interactionService 
=  !
interactionService" 4
;4 5
} 	
[ 	
HttpGet	 
] 
public 
IActionResult 
Login "
(" #
string# )
	returnUrl* 3
)3 4
{ 	
var 
	viewModel 
= 
new 
LoginViewModel  .
{ 
	ReturnUrl 
= 
	returnUrl $
} 
; 
return   
View   
(   
	viewModel    
)    !
;  ! "
}!! 	
["" 	
HttpPost""	 
]"" 
public## 
async## 
Task## 
<## 
IActionResult## '
>##' (
Login##) .
(##. /
LoginViewModel##/ =
	viewModel##> G
)##G H
{$$ 	
if%% 
(%% 
!%% 

ModelState%% 
.%% 
IsValid%% #
)%%# $
{&& 
return'' 
View'' 
('' 
	viewModel'' %
)''% &
;''& '
}(( 
var** 
user** 
=** 
await** 
_userManager** )
.**) *
FindByNameAsync*** 9
(**9 :
	viewModel**: C
.**C D
UserName**D L
)**L M
;**M N
if++ 
(++ 
user++ 
==++ 
null++ 
)++ 
{,, 

ModelState-- 
.-- 
AddModelError-- (
(--( )
string--) /
.--/ 0
Empty--0 5
,--5 6
$str--7 G
)--G H
;--H I
return.. 
View.. 
(.. 
	viewModel.. %
)..% &
;..& '
}// 
var11 
result11 
=11 
await11 
_signInManager11 -
.11- .
PasswordSignInAsync11. A
(11A B
	viewModel11B K
.11K L
UserName11L T
,11T U
	viewModel11V _
.11_ `
Password11` h
,11h i
false11j o
,11o p
false11q v
)11v w
;11w x
if22 
(22 
result22 
.22 
	Succeeded22  
)22  !
{33 
return44 
Redirect44 
(44  
	viewModel44  )
.44) *
	ReturnUrl44* 3
)443 4
;444 5
}55 

ModelState66 
.66 
AddModelError66 $
(66$ %
string66% +
.66+ ,
Empty66, 1
,661 2
$str663 @
)66@ A
;66A B
return77 
View77 
(77 
	viewModel77 !
)77! "
;77" #
}88 	
[:: 	
HttpGet::	 
]:: 
public;; 
IActionResult;; 
Register;; %
(;;% &
string;;& ,
	returnUrl;;- 6
);;6 7
{<< 	
if== 
(== 
string== 
.== 
IsNullOrEmpty== $
(==$ %
	returnUrl==% .
)==. /
)==/ 0
{>> 
string?? 
referer?? 
=??  
Request??! (
.??( )
Headers??) 0
[??0 1
$str??1 :
]??: ;
.??; <
ToString??< D
(??D E
)??E F
;??F G
if@@ 
(@@ 
!@@ 
string@@ 
.@@ 
IsNullOrEmpty@@ )
(@@) *
referer@@* 1
)@@1 2
)@@2 3
{AA 
varBB 
queryBB 
=BB 
refererBB  '
.BB' (
ToLowerBB( /
(BB/ 0
)BB0 1
.BB1 2
SplitBB2 7
(BB7 8
$strBB8 C
)BBC D
;BBD E
ifCC 
(CC 
queryCC 
.CC 
LengthCC $
>CC% &
$numCC' (
)CC( )
{DD 
	returnUrlEE !
=EE" #
queryEE$ )
[EE) *
$numEE* +
]EE+ ,
;EE, -
}FF 
}GG 
}HH 
varII 
	viewModelII 
=II 
newII 
RegisterViewModelII  1
{JJ 
	ReturnUrlKK 
=KK 
	returnUrlKK %
}LL 
;LL 
returnMM 
ViewMM 
(MM 
	viewModelMM !
)MM! "
;MM" #
}NN 	
[PP 	
HttpPostPP	 
]PP 
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
IActionResultQQ '
>QQ' (
RegisterQQ) 1
(QQ1 2
RegisterViewModelQQ2 C
	viewModelQQD M
)QQM N
{RR 	
ifTT 
(TT 
!TT 

ModelStateTT 
.TT 
IsValidTT #
)TT# $
{UU 
returnVV 
ViewVV 
(VV 
	viewModelVV %
)VV% &
;VV& '
}WW 
varXX 
userXX 
=XX 
newXX 
AppUserXX "
{YY 
UserNameZZ 
=ZZ 
	viewModelZZ $
.ZZ$ %
UserNameZZ% -
,ZZ- .
PhoneNumber[[ 
=[[ 
	viewModel[[ '
.[[' (
PhoneNumber[[( 3
,[[3 4
Email\\ 
=\\ 
	viewModel\\ !
.\\! "
Email\\" '
,\\' (
FullName]] 
=]] 
	viewModel]] %
.]]% &
FullName]]& .
}^^ 
;^^ 
var__ 
result__ 
=__ 
await__ 
_userManager__ +
.__+ ,
CreateAsync__, 7
(__7 8
user__8 <
,__< =
	viewModel__> G
.__G H
Password__H P
)__P Q
;__Q R
if`` 
(`` 
result`` 
.`` 
	Succeeded``  
)``  !
{aa 
awaitbb 
_signInManagerbb $
.bb$ %
SignInAsyncbb% 0
(bb0 1
userbb1 5
,bb5 6
falsebb7 <
)bb< =
;bb= >
returncc 
Redirectcc 
(cc  
	viewModelcc  )
.cc) *
	ReturnUrlcc* 3
)cc3 4
;cc4 5
}dd 

ModelStateee 
.ee 
AddModelErroree $
(ee$ %
stringee% +
.ee+ ,
Emptyee, 1
,ee1 2
$stree3 C
)eeC D
;eeD E
returnff 
Viewff 
(ff 
	viewModelff !
)ff! "
;ff" #
}gg 	
[ii 	
HttpGetii	 
]ii 
publicjj 
asyncjj 
Taskjj 
<jj 
IActionResultjj '
>jj' (
Logoutjj) /
(jj/ 0
stringjj0 6
logoutIdjj7 ?
)jj? @
{kk 	
awaitll 
_signInManagerll  
.ll  !
SignOutAsyncll! -
(ll- .
)ll. /
;ll/ 0
varmm 
logoutRequestmm 
=mm 
awaitmm  %
_interactionServicemm& 9
.mm9 :!
GetLogoutContextAsyncmm: O
(mmO P
logoutIdmmP X
)mmX Y
;mmY Z
returnnn 
Redirectnn 
(nn 
logoutRequestnn )
.nn) *!
PostLogoutRedirectUrinn* ?
)nn? @
;nn@ A
}oo 	
}pp 
}qq Ê
GE:\Labs\CrossPlatform\Lab6\IdentityServer\Controllers\HomeController.cs
	namespace

 	
IdentityServer


 
.

 
Controllers

 $
{ 
public 

class 
HomeController 
:  !

Controller" ,
{ 
private 
readonly 
ILogger  
<  !
HomeController! /
>/ 0
_logger1 8
;8 9
public 
HomeController 
( 
ILogger %
<% &
HomeController& 4
>4 5
logger6 <
)< =
{ 	
_logger 
= 
logger 
; 
} 	
public 
IActionResult 
Index "
(" #
)# $
{ 	
return 
View 
( 
) 
; 
} 	
public 
IActionResult 
Privacy $
($ %
)% &
{ 	
return 
View 
( 
) 
; 
} 	
[ 	
ResponseCache	 
( 
Duration 
=  !
$num" #
,# $
Location% -
=. /!
ResponseCacheLocation0 E
.E F
NoneF J
,J K
NoStoreL S
=T U
trueV Z
)Z [
][ \
public   
IActionResult   
Error   "
(  " #
)  # $
{!! 	
return"" 
View"" 
("" 
new"" 
ErrorViewModel"" *
{""+ ,
	RequestId""- 6
=""7 8
Activity""9 A
.""A B
Current""B I
?""I J
.""J K
Id""K M
??""N P
HttpContext""Q \
.""\ ]
TraceIdentifier""] l
}""m n
)""n o
;""o p
}## 	
}$$ 
}%% ˆ
7E:\Labs\CrossPlatform\Lab6\IdentityServer\Db\AppUser.cs
	namespace 	
IdentityServer
 
. 
Db 
{		 
public

 

class

 
AppUser

 
:

 
IdentityUser

 '
{ 
[ 	
Required	 
] 
public 
string 
FullName 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} ë
DE:\Labs\CrossPlatform\Lab6\IdentityServer\Db\AppUserConfiguration.cs
	namespace		 	
IdentityServer		
 
.		 
Db		 
{

 
public 

class  
AppUserConfiguration %
:& '$
IEntityTypeConfiguration( @
<@ A
AppUserA H
>H I
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
AppUser0 7
>7 8
builder9 @
)@ A
{ 	
builder 
. 
HasKey 
( 
x 
=> 
x  !
.! "
Id" $
)$ %
;% &
} 	
} 
} è
=E:\Labs\CrossPlatform\Lab6\IdentityServer\Db\AuthDbContext.cs
	namespace 	
IdentityServer
 
. 
Db 
{ 
public 

class 
AuthDbContext 
:  
IdentityDbContext! 2
<2 3
AppUser3 :
>: ;
{ 
public		 
AuthDbContext		 
(		 
DbContextOptions		 -
<		- .
AuthDbContext		. ;
>		; <
options		= D
)		D E
:		F G
base		H L
(		L M
options		M T
)		T U
{

 	
} 	
	protected 
override 
void 
OnModelCreating  /
(/ 0
ModelBuilder0 <
builder= D
)D E
{ 	
base 
. 
OnModelCreating  
(  !
builder! (
)( )
;) *
builder 
. 
Entity 
< 
AppUser "
>" #
(# $
entity$ *
=>+ -
entity. 4
.4 5
ToTable5 <
(< =
name= A
:A B
$strC J
)J K
)K L
;L M
builder 
. 
Entity 
< 
IdentityRole '
>' (
(( )
entity) /
=>0 2
entity3 9
.9 :
ToTable: A
(A B
nameB F
:F G
$strH O
)O P
)P Q
;Q R
builder 
. 
Entity 
< 
IdentityUserRole +
<+ ,
string, 2
>2 3
>3 4
(4 5
entity5 ;
=>< >
entity? E
.E F
ToTableF M
(M N
nameN R
:R S
$strT _
)_ `
)` a
;a b
builder 
. 
Entity 
< 
IdentityUserClaim ,
<, -
string- 3
>3 4
>4 5
(5 6
entity6 <
=>= ?
entity@ F
.F G
ToTableG N
(N O
nameO S
:S T
$strU `
)` a
)a b
;b c
builder 
. 
Entity 
< 
IdentityUserLogin ,
<, -
string- 3
>3 4
>4 5
(5 6
entity6 <
=>= ?
entity@ F
.F G
ToTableG N
(N O
nameO S
:S T
$strU b
)b c
)c d
;d e
builder 
. 
Entity 
< 
IdentityRoleClaim ,
<, -
string- 3
>3 4
>4 5
(5 6
entity6 <
=>= ?
entity@ F
.F G
ToTableG N
(N O
$strO [
)[ \
)\ ]
;] ^
builder 
. 
ApplyConfiguration &
(& '
new' * 
AppUserConfiguration+ ?
(? @
)@ A
)A B
;B C
} 	
} 
} ÿ
=E:\Labs\CrossPlatform\Lab6\IdentityServer\Db\DbInitializer.cs
	namespace 	
IdentityServer
 
. 
Db 
{ 
public 

class 
DbInitializer 
{ 
public 
static 
void 

Initialize %
(% &
AuthDbContext& 3
context4 ;
); <
{ 	
context 
. 
Database 
. 
EnsureCreated *
(* +
)+ ,
;, -
} 	
}		 
}

 í
BE:\Labs\CrossPlatform\Lab6\IdentityServer\Models\ErrorViewModel.cs
	namespace 	
IdentityServer
 
. 
Models 
{ 
public 

class 
ErrorViewModel 
{ 
public 
string 
	RequestId 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
bool		 
ShowRequestId		 !
=>		" $
!		% &
string		& ,
.		, -
IsNullOrEmpty		- :
(		: ;
	RequestId		; D
)		D E
;		E F
}

 
} ›
BE:\Labs\CrossPlatform\Lab6\IdentityServer\Models\LoginViewModel.cs
	namespace 	
IdentityServer
 
. 
Models 
{ 
public 

class 
LoginViewModel 
{ 
[ 	
Required	 
] 
public 
string 
UserName 
{  
get! $
;$ %
set& )
;) *
}+ ,
[		 	
Required			 
]		 
[

 	
DataType

	 
(

 
DataType

 
.

 
Password

 #
)

# $
]

$ %
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
	ReturnUrl 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} ö
EE:\Labs\CrossPlatform\Lab6\IdentityServer\Models\RegisterViewModel.cs
	namespace 	
IdentityServer
 
. 
Models 
{ 
public 

class 
RegisterViewModel "
{ 
[ 	
Required	 
] 
public 
string 
UserName 
{  
get! $
;$ %
set& )
;) *
}+ ,
[		 	
Required			 
]		 
[

 	
DataType

	 
(

 
DataType

 
.

 
Password

 #
)

# $
]

$ %
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
] 
public 
string 
FullName 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
] 
public 
string 
PhoneNumber !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Required	 
] 
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
] 
[ 	
Compare	 
( 
$str 
) 
] 
[ 	
DataType	 
( 
DataType 
. 
Password #
)# $
]$ %
public 
string 
ConfirmPassword %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
	ReturnUrl 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} ÷
;E:\Labs\CrossPlatform\Lab6\IdentityServer\ProfileService.cs
	namespace 	
IdentityServer
 
{ 
public 

class 
ProfileService 
:  !
IProfileService" 1
{ 
	protected 
UserManager 
< 
AppUser %
>% &
UserManager' 2
;2 3
public 
ProfileService 
( 
UserManager )
<) *
AppUser* 1
>1 2
userManager3 >
)> ?
{ 	
UserManager 
= 
userManager %
;% &
} 	
public 
async 
Task 
GetProfileDataAsync -
(- .%
ProfileDataRequestContext. G
contextH O
)O P
{ 	
var 
appUser 
= 
await 
UserManager  +
.+ ,
GetUserAsync, 8
(8 9
context9 @
.@ A
SubjectA H
)H I
;I J
var 
claims 
= 
new 
List !
<! "
Claim" '
>' (
{ 
new 
Claim 
( 
$str $
,$ %
appUser& -
.- .
UserName. 6
)6 7
,7 8
new 
Claim 
( 
$str $
,$ %
appUser& -
.- .
FullName. 6
)6 7
,7 8
new 
Claim 
( 
$str )
,) *
appUser+ 2
.2 3
Email3 8
)8 9
,9 :
new 
Claim 
( 
$str !
,! "
appUser# *
.* +
PhoneNumber+ 6
)6 7
} 
; 
context 
. 
IssuedClaims  
.  !
AddRange! )
() *
claims* 0
)0 1
;1 2
}   	
public"" 
async"" 
Task"" 
IsActiveAsync"" '
(""' (
IsActiveContext""( 7
context""8 ?
)""? @
{## 	
var$$ 
user$$ 
=$$ 
await$$ 
UserManager$$ (
.$$( )
GetUserAsync$$) 5
($$5 6
context$$6 =
.$$= >
Subject$$> E
)$$E F
;$$F G
context&& 
.&& 
IsActive&& 
=&& 
(&&  
user&&  $
!=&&% '
null&&( ,
)&&, -
;&&- .
}'' 	
}(( 
})) ï
4E:\Labs\CrossPlatform\Lab6\IdentityServer\Program.cs
	namespace 	
IdentityServer
 
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	
var 
host 
= 
CreateHostBuilder (
(( )
args) -
)- .
.. /
Build/ 4
(4 5
)5 6
;6 7
using 
( 
var 
scope 
= 
host #
.# $
Services$ ,
., -
CreateScope- 8
(8 9
)9 :
): ;
{ 
var 
serviceProvider #
=$ %
scope& +
.+ ,
ServiceProvider, ;
;; <
try 
{ 
var 
context 
=  !
serviceProvider" 1
.1 2
GetRequiredService2 D
<D E
AuthDbContextE R
>R S
(S T
)T U
;U V
DbInitializer !
.! "

Initialize" ,
(, -
context- 4
)4 5
;5 6
} 
catch 
( 
	Exception  
e! "
)" #
{ 
Console 
. 
	WriteLine %
(% &
e& '
)' (
;( )
throw 
; 
} 
}   
host"" 
."" 
Run"" 
("" 
)"" 
;"" 
}## 	
public%% 
static%% 
IHostBuilder%% "
CreateHostBuilder%%# 4
(%%4 5
string%%5 ;
[%%; <
]%%< =
args%%> B
)%%B C
=>%%D F
Host&& 
.&&  
CreateDefaultBuilder&& %
(&&% &
args&&& *
)&&* +
.'' $
ConfigureWebHostDefaults'' )
('') *

webBuilder''* 4
=>''5 7
{(( 

webBuilder)) 
.)) 

UseStartup)) )
<))) *
Startup))* 1
>))1 2
())2 3
)))3 4
;))4 5
}** 
)** 
;** 
}++ 
},, ˘,
4E:\Labs\CrossPlatform\Lab6\IdentityServer\Startup.cs
	namespace 	
IdentityServer
 
{ 
public 

class 
Startup 
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
void 
ConfigureServices %
(% &
IServiceCollection& 8
services9 A
)A B
{ 	
var 
connectionString  
=! "
Configuration# 0
.0 1
GetValue1 9
<9 :
string: @
>@ A
(A B
$strB P
)P Q
;Q R
services 
. 
AddDbContext !
<! "
AuthDbContext" /
>/ 0
(0 1
options1 8
=>9 ;
options< C
.C D
	UseSqliteD M
(M N
connectionStringN ^
)^ _
)_ `
;` a
services 
. 
AddIdentity  
<  !
AppUser! (
,( )
IdentityRole* 6
>6 7
(7 8
config8 >
=>? A
{ 
config   
.   
Password   #
.  # $
RequiredLength  $ 2
=  3 4
$num  5 6
;  6 7
config!! 
.!! 
Password!! #
.!!# $
RequireDigit!!$ 0
=!!1 2
false!!3 8
;!!8 9
config"" 
."" 
Password"" #
.""# $"
RequireNonAlphanumeric""$ :
=""; <
false""= B
;""B C
config## 
.## 
Password## #
.### $
RequireUppercase##$ 4
=##5 6
false##7 <
;##< =
}$$ 
)$$ 
.%% $
AddEntityFrameworkStores%% )
<%%) *
AuthDbContext%%* 7
>%%7 8
(%%8 9
)%%9 :
.&& $
AddDefaultTokenProviders&& )
(&&) *
)&&* +
;&&+ ,
services(( 
.(( 
AddIdentityServer(( &
(((& '
config((' -
=>((. 0
{)) 
config** 
.** 
UserInteraction** *
.*** +
LoginUrl**+ 3
=**4 5
$str**6 C
;**C D
}++ 
)++ 
.,, 
AddAspNetIdentity,, "
<,," #
AppUser,,# *
>,,* +
(,,+ ,
),,, -
.-- 
AddInMemoryClients-- #
(--# $
IdentityServer--$ 2
.--2 3
Configuration--3 @
.--@ A

GetClients--A K
(--K L
)--L M
)--M N
... #
AddInMemoryApiResources.. (
(..( )
IdentityServer..) 7
...7 8
Configuration..8 E
...E F
GetApiResources..F U
(..U V
)..V W
)..W X
.//  
AddInMemoryApiScopes// %
(//% &
IdentityServer//& 4
.//4 5
Configuration//5 B
.//B C
GetApiScopes//C O
(//O P
)//P Q
)//Q R
.00 (
AddInMemoryIdentityResources00 -
(00- .
IdentityServer00. <
.00< =
Configuration00= J
.00J K 
GetIdentityResources00K _
(00_ `
)00` a
)00a b
.11 )
AddDeveloperSigningCredential11 .
(11. /
)11/ 0
.22 
AddProfileService22 "
<22" #
ProfileService22# 1
>221 2
(222 3
)223 4
;224 5
services33 
.33 #
AddControllersWithViews33 ,
(33, -
)33- .
;33. /
}44 	
public77 
void77 
	Configure77 
(77 
IApplicationBuilder77 1
app772 5
,775 6
IWebHostEnvironment777 J
env77K N
)77N O
{88 	
if99 
(99 
env99 
.99 
IsDevelopment99 !
(99! "
)99" #
)99# $
{:: 
app;; 
.;; %
UseDeveloperExceptionPage;; -
(;;- .
);;. /
;;;/ 0
}<< 
else== 
{>> 
app?? 
.?? 
UseExceptionHandler?? '
(??' (
$str??( 5
)??5 6
;??6 7
}@@ 
appAA 
.AA 
UseStaticFilesAA 
(AA 
)AA  
;AA  !
appCC 
.CC 
UseIdentityServerCC !
(CC! "
)CC" #
;CC# $
appEE 
.EE 

UseRoutingEE 
(EE 
)EE 
;EE 
appGG 
.GG 
UseAuthenticationGG !
(GG! "
)GG" #
;GG# $
appII 
.II 
UseAuthorizationII  
(II  !
)II! "
;II" #
appKK 
.KK 
UseEndpointsKK 
(KK 
	endpointsKK &
=>KK' )
{LL 
	endpointsMM 
.MM 
MapControllerRouteMM ,
(MM, -
nameNN 
:NN 
$strNN #
,NN# $
patternOO 
:OO 
$strOO E
)OOE F
;OOF G
}PP 
)PP 
;PP 
}QQ 	
}RR 
}SS 