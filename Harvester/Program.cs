using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetDimension.Weibo;
using NetDimension.Weibo.Entities;

namespace TestClassicalSDKConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //新浪微博SDK for .net 2.0/3.5/4.0
            //之前专门针对.net4.0动态类型特性的版本在未来的release中会同时支持返回dynamic和实体类
            OAuth oauth = new OAuth("AppKey", "AppSecret");

            var result = oauth.ClientLogin("<账号>", "<密码>", "<绑定的回调地址>");
            //标准授权及登录流程请下载前面提供的DEMO程序查看。
            if (result)
            {
                try
                {
                    var Sina = new Client(oauth);

                    //NetDimension.Weibo.Entities.RateLimitStatus limit = Sina.API.Account.RateLimitStatus();
                    var limit = Sina.API.Account.RateLimitStatus();//与之前返回dynamic相比，这个版本返回了实体类型
                    Console.WriteLine("======= 用量 =======");
                    Console.WriteLine("IPLimit:{0}", limit.IPLimit);
                    Console.WriteLine("RemainingIPHits:{0}", limit.RemainingIPHits);
                    Console.WriteLine("RemainingUserHits:{0}", limit.RemainingUserHits);
                    Console.WriteLine("ResetTime:{0}", limit.ResetTime);
                    Console.WriteLine("UserLimit:{0}", limit.UserLimit);
                    //发布一条微博看看
                    var status = Sina.API.Statuses.Update(string.Format("这条测试微博在{0}发自《新浪微博SDK for .Net 2.0/3.5/4.0》，由新浪网云南频道技术部林选臣开发制作，欢迎下载试用。下载地址：http://weibosdk.codeplex.com", DateTime.Now.ToString()));
                    //返回的status类型为NetDimension.Weibo.Entities.status.Entity
                    //在这里设置一个断点，看看status吧^_^

                    Console.WriteLine("======= 微博结果 =======");
                    Console.WriteLine("ID:{0}", status.ID);
                    Console.WriteLine("MID:{0}", status.MID);


                }
                catch (WeiboException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadKey();
        }
    }
}