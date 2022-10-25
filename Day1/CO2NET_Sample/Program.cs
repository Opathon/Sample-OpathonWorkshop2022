#region Start

////Reference Configuration File
//var configBuilder = new ConfigurationBuilder();
//configBuilder.AddJsonFile("appsettings.json", false, false);
//Console.WriteLine("Added appsettings.json");

////Build
//var config = configBuilder.Build();
//Console.WriteLine("Finish ServiceCollection & ConfigurationBuilder Init");

////Bind Configuration
//var senparcSetting = new SenparcSetting();
//config.GetSection("SenparcSetting").Bind(senparcSetting);

////Register Service
//var services = new ServiceCollection();
//services.AddMemoryCache();//Use Local Memory

////CO2NET Global Register
//services.AddSenparcGlobalServices(config);//Senparc.CO2NET Global Register
//Console.WriteLine("Finish AddSenparcGlobalServices Register");

//// Start CO2NET
//IRegisterService register =
//    RegisterService.Start(senparcSetting)
//                   .UseSenparcGlobal();

//var serviceProvider = services.BuildServiceProvider();

#endregion

Console.WriteLine("Hello, Opathon!");

#region JSON Serialize & Deserialization

//var studentList = Student.StudentList;
//var studentListStr = studentList.ToJson();// true / false
//Console.WriteLine(studentListStr);

//studentListStr = "[{\"Id\":1,\"Name\":\"Mark\",\"Age\":21},{\"Id\":2,\"Name\":\"John\",\"Age\":16},{\"Id\":3,\"Name\":\"Kuke\",\"Age\":18}]";
//var newStudentList = studentListStr.GetObject<List<Student>>();
//Console.WriteLine($"Count:{newStudentList.Count()}");
//Console.WriteLine($"Mark Age:{newStudentList.FirstOrDefault(z => z.Name == "Mark")?.Age}");

#endregion

#region Encrypt

//var md5Str = Senparc.CO2NET.Helpers.EncryptHelper.GetMD5("123456");
//Console.WriteLine($"MD5:{md5Str}");

#endregion

#region HTTP Spider

//var url = @"https://opathon.senparc.com";//https://www.opathon.com
//var html = await Senparc.CO2NET.HttpUtility.RequestUtility.HttpGetAsync(serviceProvider, url, cookieContainer: null);
//Console.WriteLine("HTML:");
//Console.WriteLine(html);

#endregion

#region SenparcTrace

//try
//{
//    SenparcTrace.SendCustomLog("Normal Trace Test", $"Now:{SystemTime.Now}");
//    throw new Exception("Throw an Exception");
//}
//catch (Exception ex)
//{
//    SenparcTrace.SendCustomLog("Trace", ex.Message);
//}

#endregion

#region MessageQuque

//var smq = new SenparcMessageQueue();

//smq.Add("load Opathon Site", async () =>
//{
//    var url = "https://opathon.senparc.com";
//    Console.WriteLine("Loading HTML:");
//    var dt = SystemTime.Now;
//    var html = await Senparc.CO2NET.HttpUtility.RequestUtility.HttpGetAsync(serviceProvider, url, cookieContainer: null);
//    Console.WriteLine(html);
//    Console.WriteLine($"Cost time: {SystemTime.DiffTotalMS(dt)}ms");
//});

//Console.WriteLine("Go ahead.");

#endregion

//Console.ReadKey();