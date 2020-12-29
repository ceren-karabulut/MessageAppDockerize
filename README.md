# MessageAppDockerize
Ef Core,Docker,Jwt,Serilog ve Sql Server kullanilmis bir mesajlasma api'si.Kullanicilar oturum acip mesaj gonderip alabilir ve mesajlasma gecminisini goruntuleyebilir.

Uygulama Docker Compose ile calistirilabilir.

<h1>Endpoint Routemap</h1>
Uygulama ayaga kalktiginde sizi Swagger ekrani  karsilar.

![messageapp_screenshot](https://user-images.githubusercontent.com/57019480/103289147-89a56600-49f7-11eb-8666-f84e7b71fd1c.PNG)


<h3>User</h3>
-register:username ve password girilerek register olunur.<br>
-login:Username ve password girilerek login olunur.Basarili login sonucu jwt key'i doner.<br>


<h3>Message</h3>
Message endpointlerini kullanabilmek register olduktan sonre da login olup donen key ile sag ustteki authorize butonuna basıp authorize olmak gereklidir.Asagidaki sekilde authorize olunur.<br><br>

![login_ss](https://user-images.githubusercontent.com/57019480/103288984-303d3700-49f7-11eb-9e15-ea5af887c879.PNG) <br><br>

![logout_ss](https://user-images.githubusercontent.com/57019480/103288996-36331800-49f7-11eb-9aa4-b1d034537b35.PNG)



-sendmessage: Kullanici, mesajı gondermek istedigi kullanici adini ve mesajini girerek mesaj gonderebilir.<br>
-messagehistories: Kullanicinin mesaj gecmisini goruntuler.<br>
-blockuser:Kullanici engellemek istedigi kullanici adini girerek o kullanicini kendisine mesaj atmasini engelleyebilir. 
