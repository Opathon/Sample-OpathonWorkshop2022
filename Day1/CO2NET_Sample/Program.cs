#region Start

//Reference Configuration File
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.HttpUtility;

var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json", false, false);
Console.WriteLine("Added appsettings.json");

//Build
var config = configBuilder.Build();
Console.WriteLine("Finish ServiceCollection & ConfigurationBuilder Init");

//Bind Configuration
var senparcSetting = new SenparcSetting();
config.GetSection("SenparcSetting").Bind(senparcSetting);

//Register Service
var services = new ServiceCollection();
services.AddMemoryCache();//Use Local Memory

//CO2NET Global Register
services.AddSenparcGlobalServices(config);//Senparc.CO2NET Global Register
Console.WriteLine("Finish AddSenparcGlobalServices Register");

// Start CO2NET
IRegisterService register =
    RegisterService.Start(senparcSetting)
                   .UseSenparcGlobal();

var serviceProvider = services.BuildServiceProvider();

#endregion


#region Setup Redis Cache

//配置全局使用Redis缓存（按需，独立）

//register.ChangeDefaultCacheNamespace("Opathon");
//Console.WriteLine($"Cache namespace changed：{Senparc.CO2NET.Config.DefaultCacheNamespace}");

//var redisConfigurationStr = senparcSetting.Cache_Redis_Configuration;
//Senparc.CO2NET.Cache.Redis.Register.SetConfigurationOption(redisConfigurationStr);
//Console.WriteLine("Finish Redis setting");


////以下会立即将全局缓存设置为 Redis
//Senparc.CO2NET.Cache.Redis.Register.UseKeyValueRedisNow();//键值对缓存策略（推荐）
//Console.WriteLine("Start Redis UseKeyValue mode");

////Senparc.CO2NET.Cache.Redis.Register.UseHashRedisNow();//HashSet储存格式的缓存策略

//var cache = Senparc.CO2NET.Cache.CacheStrategyFactory.GetObjectCacheStrategyInstance();
//Console.WriteLine("Current Cache Strategy 1:" + cache.GetType().Name);
//await cache.SetAsync("aa", 11);

//CacheStrategyFactory.RegisterObjectCacheStrategy(() => LocalObjectCacheStrategy.Instance);
//cache = Senparc.CO2NET.Cache.CacheStrategyFactory.GetObjectCacheStrategyInstance();
//Console.WriteLine("Current Cache Strategy 2:" + cache.GetType().Name);

//CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
//cache = Senparc.CO2NET.Cache.CacheStrategyFactory.GetObjectCacheStrategyInstance();
//Console.WriteLine("Current Cache Strategy 3:" + cache.GetType().Name);


#endregion

Console.WriteLine("Hello, Opathon!");

#region JSON Serialize & Deserialization

//var studentList = Student.StudentList;
//var studentListStr = studentList.ToJson(true);// true / false
//Console.WriteLine(studentListStr);

//var newStudentListStr = "[{\"Id\":1,\"Name\":\"Mark\",\"Age\":21},{\"Id\":2,\"Name\":\"John\",\"Age\":16},{\"Id\":3,\"Name\":\"Kuke\",\"Age\":18}]";
//var newStudentList = newStudentListStr.GetObject<List<Student>>();
//Console.WriteLine($"Count:{newStudentList.Count()}");
//var item = newStudentList.FirstOrDefault(z => z.Name == "Mark");
//Console.WriteLine($"Mark Age:{item?.Age}");

#endregion

#region Encrypt

//var md5Str = EncryptHelper.GetMD5("123456");
//Console.WriteLine($"MD5:{md5Str}");

#endregion

#region HTTP Spider

//var url = @"https://www.baidu.com";//https://www.opathon.com
//var html = await RequestUtility.HttpGetAsync(serviceProvider, url, cookieContainer: null, timeOut: 10000);
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
//    SenparcTrace.SendCustomLog("Trace", ex.Message+"\r\n"+ex.StackTrace);
//}

#endregion

#region MessageQuque

//var smq = new SenparcMessageQueue();

//Action<string> act = async (url) =>
//{
//    //var url = "https://opathon.senparc.com";
//    Console.WriteLine("Loading HTML " + url);
//    var dt = SystemTime.Now;
//    var html = await RequestUtility.HttpGetAsync(serviceProvider, url, cookieContainer: null);
//    Console.WriteLine($"Cost time: {SystemTime.DiffTotalMS(dt)}ms");
//    Console.WriteLine(url + html);
//};

//smq.Add("load Opathon Site 3", async () =>
//{
//    act("https://www.opathon.com");
//});

//smq.Add("load Opathon Site 1", async () =>
//{
//    act("https://opathon.senparc.com");
//});

//smq.Add("load Opathon Site 2", async () =>
//{
//    act("https://opathon.senparc.com");
//});



//Console.WriteLine("Go ahead.");

#endregion

Console.ReadKey();
