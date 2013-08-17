using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fluent;
using System.Collections.ObjectModel;

namespace BlackApplePlayer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {

        private ObservableCollection<TVInfo> Tvinfos = new ObservableCollection<TVInfo>();
        private ObservableCollection<Configure> configs = new ObservableCollection<Configure>();

        public MainWindow()
        {
            //初始化以前更新配置信息
            //获取程序当前运行路径
            string str = System.Environment.CurrentDirectory + "\\configure.ini";
            if (File.Exists(str))
            {

                using (StreamReader sr = new StreamReader(str))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "ThemeStyle=Blue")
                        {
                            configs.Add(new Configure() { Key = "ThemeStyle", Value = "Blue" });
                        }
                        if (line == "ThemeStyle=Black")
                        {
                            configs.Add(new Configure() { Key = "ThemeStyle", Value = "Black" });
                        }
                        if (line == "ThemeStyle=Silver")
                        {
                            configs.Add(new Configure() { Key = "ThemeStyle", Value = "Silver" });
                        }

                        if (line == "DeleBuffer=DeleRBtn")
                        {
                            configs.Add(new Configure() { Key = "DeleBuffer", Value = "DeleRBtn" });
                        }
                        if (line == "DeleBuffer=KeepRBtn")
                        {
                            configs.Add(new Configure() { Key = "DeleBuffer", Value = "KeepRBtn" });
                        }
                    }
                }


            }
            //设置默认值
            else
            {
                configs.Add(new Configure() { Key = "ThemeStyle", Value = "Silver" });
                configs.Add(new Configure() { Key = "DeleBuffer", Value = "DeleRBtn" });
            }


            InitializeComponent();

           


            //加载配置信息
            LoadConfig();
            DeleteTemp();






            TVMediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(TVMediaPlayer_PlayStateChange);
            //TVMediaPlayer.enableContextMenu = false;

            TVMediaPlayer.uiMode = "mini";
            SearchExpander.IsExpanded = true;

           

            Tv_load();
            foreach (TVInfo tvinfo in Tvinfos)
            {
                if (tvinfo.Name != "BlackApplePlayer" && !tvinfo.Name.Contains("__"))
                {
                    listBox.Items.Add(tvinfo.Name);
                }
                
            }

        }

        private void TVMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //用于非微软编码，当缓冲后播放
            if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsBuffering) TVMediaPlayer.Ctlcontrols.play();
        }
        public class TVInfo
        {
            public string Name
            {
                get;
                set;
            }

            public string Url
            {
                get;
                set;
            }
        }
        public class Configure
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
        public ObservableCollection<TVInfo> Tv_load()
        {

            //设置一个空的节目
            Tvinfos.Add(new TVInfo() { Name = "BlackApplePlayer", Url = "http://127.0.0.1" });


            //设置奥运特别节目
            Tvinfos.Add(new TVInfo() { Name = "央视综合__", Url = "http://[2001:da8:8000:6007::242]:3002" });
            Tvinfos.Add(new TVInfo() { Name = "央视综合_北邮__", Url = "mms://officetv.bupt.edu.cn/CCTV-1" });
            Tvinfos.Add(new TVInfo() { Name = "央视综合_大连理工__", Url = " mms://vod2.dlut.edu.cn/CCTV1" });

            Tvinfos.Add(new TVInfo() { Name = "央视高清__", Url = "http://media2.sjtu.edu.cn:3090" });
            Tvinfos.Add(new TVInfo() { Name = "央视高清_北邮__", Url = "mms://officetv.bupt.edu.cn/CCTV-HD" });
            Tvinfos.Add(new TVInfo() { Name = "央视高清_兰州大学__", Url = "mms://tv.lzu.edu.cn/cctv-hd" });
            Tvinfos.Add(new TVInfo() { Name = "央视高清_华南师范__", Url = "mms://[2001:da8:200b:120e::226]/cctv-hd" });
            Tvinfos.Add(new TVInfo() { Name = "央视高清_大连理工__", Url = " mms://vod2.dlut.edu.cn/CCTVHDTV" });

            Tvinfos.Add(new TVInfo() { Name = "军事农业__", Url = "http://[2001:da8:8000:6007::242]:3002" });
            Tvinfos.Add(new TVInfo() { Name = "军事农业_北邮__", Url = "mms://officetv.bupt.edu.cn/CCTV-7" });
            Tvinfos.Add(new TVInfo() { Name = "军事农业_大连理工__", Url = " mms://vod2.dlut.edu.cn/CCTV7" });

            //
            Tvinfos.Add(new TVInfo() { Name = "央视综合", Url = "http://[2001:da8:8000:6007::242]:3002" });
            Tvinfos.Add(new TVInfo() { Name = "央视财经", Url = "http://[2001:da8:8000:6007::242]:3003" });
            Tvinfos.Add(new TVInfo() { Name = "央视综艺", Url = "http://media0.sjtu.edu.cn:3065" });
            Tvinfos.Add(new TVInfo() { Name = "中文国际", Url = "http://media0.sjtu.edu.cn:3066" });
            Tvinfos.Add(new TVInfo() { Name = "央视体育", Url = "http://[2001:da8:8000:6007::242]:3004" });
            Tvinfos.Add(new TVInfo() { Name = "央视电影", Url = "http://[2001:da8:8000:6007::242]:3005" });
            Tvinfos.Add(new TVInfo() { Name = "军事农业", Url = "http://[2001:da8:8000:6007::241]:3067" });
            Tvinfos.Add(new TVInfo() { Name = "央视电视剧", Url = "http://[2001:da8:8000:6007::242]:3006" });
            Tvinfos.Add(new TVInfo() { Name = "央视News", Url = "http://[2001:da8:8000:6007::242]:3007" });
            Tvinfos.Add(new TVInfo() { Name = "央视高清", Url = "http://media2.sjtu.edu.cn:3090" });


            Tvinfos.Add(new TVInfo() { Name = "湖南卫视", Url = "http://[2001:da8:8000:6007::244]:3128" });
            Tvinfos.Add(new TVInfo() { Name = "东方卫视", Url = "http://[2001:da8:8000:6007::242]:3020" });
            Tvinfos.Add(new TVInfo() { Name = "北京卫视", Url = "http://[2001:da8:8000:6007::243]:3084" });

            Tvinfos.Add(new TVInfo() { Name = "吉林卫视", Url = "http://[2001:da8:8000:6007::243]:3083" });
            Tvinfos.Add(new TVInfo() { Name = "湖北卫视", Url = "http://[2001:da8:8000:6007::244]:3129" });
            Tvinfos.Add(new TVInfo() { Name = "山西卫视", Url = "http://[2001:da8:8000:6007::243]:3085" });
            Tvinfos.Add(new TVInfo() { Name = "天津卫视", Url = "http://[2001:da8:8000:6007::243]:3086" });
            Tvinfos.Add(new TVInfo() { Name = "河北卫视", Url = "http://[2001:da8:8000:6007::243]:3087" });
            Tvinfos.Add(new TVInfo() { Name = "山东卫视", Url = "http://[2001:da8:8000:6007::243]:3088" });
            Tvinfos.Add(new TVInfo() { Name = "重庆卫视", Url = "http://[2001:da8:8000:6007::244]:3116" });
            Tvinfos.Add(new TVInfo() { Name = "江苏卫视", Url = "http://[2001:da8:8000:6007::244]:3104" });
            Tvinfos.Add(new TVInfo() { Name = "黑龙江卫视", Url = "http://[2001:da8:8000:6007::243]:3101" });
            Tvinfos.Add(new TVInfo() { Name = "浙江卫视", Url = "http://[2001:da8:8000:6007::244]:3118" });
            Tvinfos.Add(new TVInfo() { Name = "广西卫视", Url = "http://[2001:da8:8000:6007::243]:3102" });

            Tvinfos.Add(new TVInfo() { Name = "内蒙古卫视", Url = "http://[2001:da8:8000:6007::244]:3120" });
            Tvinfos.Add(new TVInfo() { Name = "广东卫视", Url = "http://[2001:da8:8000:6007::244]:3121" });
            Tvinfos.Add(new TVInfo() { Name = "四川卫视", Url = "http://[2001:da8:8000:6007::244]:3130" });
            Tvinfos.Add(new TVInfo() { Name = "江西卫视", Url = "http://[2001:da8:8000:6007::244]:3131" });
            Tvinfos.Add(new TVInfo() { Name = "东南卫视", Url = "http://[2001:da8:8000:6007::244]:3132" });
            Tvinfos.Add(new TVInfo() { Name = "青海卫视", Url = "http://[2001:da8:8000:6007::244]:3133" });
            Tvinfos.Add(new TVInfo() { Name = "河南卫视", Url = "http://[2001:da8:8000:6007::244]:3134" });
            Tvinfos.Add(new TVInfo() { Name = "新疆卫视", Url = "http://[2001:da8:8000:6007::244]:3135" });
            Tvinfos.Add(new TVInfo() { Name = "贵州卫视", Url = "http://[2001:da8:8000:6007::244]:3136" });
            Tvinfos.Add(new TVInfo() { Name = "旅游卫视", Url = "http://[2001:da8:8000:6007::244]:3114" });
            Tvinfos.Add(new TVInfo() { Name = "宁夏卫视", Url = "http://[2001:da8:8000:6007::244]:3113" });
            Tvinfos.Add(new TVInfo() { Name = "陕西卫视", Url = "http://[2001:da8:8000:6007::244]:3103" });
            Tvinfos.Add(new TVInfo() { Name = "安徽卫视", Url = "http://[2001:da8:8000:6007::244]:3105" });
            Tvinfos.Add(new TVInfo() { Name = "辽宁卫视", Url = "http://[2001:da8:8000:6007::244]:3119" });
            Tvinfos.Add(new TVInfo() { Name = "云南卫视", Url = "http://[2001:da8:8000:6007::244]:3117" });
            Tvinfos.Add(new TVInfo() { Name = "甘肃卫视", Url = "http://[2001:da8:8000:6007::244]:3115" });
            Tvinfos.Add(new TVInfo() { Name = "西藏卫视", Url = "http://[2001:da8:8000:6007::244]:3112" });


            //地方高清





            //标清电台
            Tvinfos.Add(new TVInfo() { Name = "央视综合_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-1" });
            Tvinfos.Add(new TVInfo() { Name = "央视财经_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-2" });
            Tvinfos.Add(new TVInfo() { Name = "央视综艺_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-3" });
            Tvinfos.Add(new TVInfo() { Name = "中文国际_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-4" });
            Tvinfos.Add(new TVInfo() { Name = "央视体育_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-5" });
            Tvinfos.Add(new TVInfo() { Name = "央视电影_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-6" });
            Tvinfos.Add(new TVInfo() { Name = "军事农业_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-7" });
            Tvinfos.Add(new TVInfo() { Name = "央视电视剧_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-8" });
            Tvinfos.Add(new TVInfo() { Name = "英语国际_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-9" });
            Tvinfos.Add(new TVInfo() { Name = "科学教育_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-10" });
            Tvinfos.Add(new TVInfo() { Name = "央视戏曲_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-11" });
            Tvinfos.Add(new TVInfo() { Name = "社会与法_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-12" });
            Tvinfos.Add(new TVInfo() { Name = "央视新闻_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-news" });
            Tvinfos.Add(new TVInfo() { Name = "央视音乐_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-music" });
            Tvinfos.Add(new TVInfo() { Name = "央视少儿_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-kid" });
            Tvinfos.Add(new TVInfo() { Name = "中国教育1_北邮", Url = "mms://officetv.bupt.edu.cn/CETV-1" });
            Tvinfos.Add(new TVInfo() { Name = "中国教育3_北邮", Url = "mms://officetv.bupt.edu.cn/CETV-3" });
            Tvinfos.Add(new TVInfo() { Name = "央视高清_北邮", Url = "mms://officetv.bupt.edu.cn/CCTV-HD" });

            //北京_北邮
            Tvinfos.Add(new TVInfo() { Name = "BTV1_北邮", Url = "mms://officetv.bupt.edu.cn/BTV-1" });
            Tvinfos.Add(new TVInfo() { Name = "BTV2_北邮", Url = "mms://officetv.bupt.edu.cn/BTV-2" });
            Tvinfos.Add(new TVInfo() { Name = "BTV3_北邮", Url = "mms://officetv.bupt.edu.cn/BTV-3" });
            Tvinfos.Add(new TVInfo() { Name = "BTV4_北邮", Url = "mms://officetv.bupt.edu.cn/BTV-4" });
            Tvinfos.Add(new TVInfo() { Name = "BTV5_北邮", Url = "mms://officetv.bupt.edu.cn/BTV-5" });
            Tvinfos.Add(new TVInfo() { Name = "BTV6_北邮", Url = "mms://officetv.bupt.edu.cn/BTV-6" });
            Tvinfos.Add(new TVInfo() { Name = "BTV7_北邮", Url = "mms://officetv.bupt.edu.cn/BTV-7" });
            Tvinfos.Add(new TVInfo() { Name = "BTV8_北邮", Url = "mms://officetv.bupt.edu.cn/BTV-8" });
            Tvinfos.Add(new TVInfo() { Name = "海淀有线_北邮", Url = "mms://officetv.bupt.edu.cn/BTV-9" });

            //地方卫视_北邮

            Tvinfos.Add(new TVInfo() { Name = "湖南卫视_北邮", Url = "mms://officetv.bupt.edu.cn/HNWS" });
            Tvinfos.Add(new TVInfo() { Name = "湖北卫视_北邮", Url = "mms://officetv.bupt.edu.cn/hbWS" });
            Tvinfos.Add(new TVInfo() { Name = "辽宁卫视_北邮", Url = "mms://officetv.bupt.edu.cn/lnWS" });
            Tvinfos.Add(new TVInfo() { Name = "东方卫视_北邮", Url = "mms://officetv.bupt.edu.cn/shWS" });
            Tvinfos.Add(new TVInfo() { Name = "云南卫视_北邮", Url = "mms://officetv.bupt.edu.cn/ynWS" });
            Tvinfos.Add(new TVInfo() { Name = "东南卫视_北邮", Url = "mms://officetv.bupt.edu.cn/fjWS" });
            Tvinfos.Add(new TVInfo() { Name = "山东卫视_北邮", Url = "mms://officetv.bupt.edu.cn/sdWS" });
            Tvinfos.Add(new TVInfo() { Name = "安徽卫视_北邮", Url = "mms://officetv.bupt.edu.cn/ahWS" });
            Tvinfos.Add(new TVInfo() { Name = "浙江卫视_北邮", Url = "mms://officetv.bupt.edu.cn/zjWS" });
            Tvinfos.Add(new TVInfo() { Name = "重庆卫视_北邮", Url = "mms://officetv.bupt.edu.cn/cqWS" });
            Tvinfos.Add(new TVInfo() { Name = "江苏卫视_北邮", Url = "mms://officetv.bupt.edu.cn/jsWS" });
            Tvinfos.Add(new TVInfo() { Name = "江西卫视_北邮", Url = "mms://officetv.bupt.edu.cn/jxws" });
            Tvinfos.Add(new TVInfo() { Name = "天津卫视_北邮", Url = "mms://officetv.bupt.edu.cn/TJWS" });
            Tvinfos.Add(new TVInfo() { Name = "卡酷动画_北邮", Url = "mms://officetv.bupt.edu.cn/KKDH" });
            Tvinfos.Add(new TVInfo() { Name = "旅游卫视_北邮", Url = "mms://officetv.bupt.edu.cn/LYWS" });


            //大连理工-标清
            Tvinfos.Add(new TVInfo() { Name = "央视综合_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV1" });
            Tvinfos.Add(new TVInfo() { Name = "央视财经_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV2" });
            Tvinfos.Add(new TVInfo() { Name = "央视综艺_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV3" });
            Tvinfos.Add(new TVInfo() { Name = "中文国际_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV4" });
            Tvinfos.Add(new TVInfo() { Name = "央视体育_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV5" });
            Tvinfos.Add(new TVInfo() { Name = "央视电影_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV6" });
            Tvinfos.Add(new TVInfo() { Name = "军事农业_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV7" });
            Tvinfos.Add(new TVInfo() { Name = "央视电视剧_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV8" });
            Tvinfos.Add(new TVInfo() { Name = "科学教育_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV10" });
            Tvinfos.Add(new TVInfo() { Name = "央视戏曲_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV11" });
            Tvinfos.Add(new TVInfo() { Name = "社会与法_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV12" });
            Tvinfos.Add(new TVInfo() { Name = "英语国际_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTV9" });
            Tvinfos.Add(new TVInfo() { Name = "央视高清_大连理工", Url = " mms://vod2.dlut.edu.cn/CCTVHDTV" });
            Tvinfos.Add(new TVInfo() { Name = "中国教育1_大连理工", Url = " mms://vod2.dlut.edu.cn/CETV1" });
            Tvinfos.Add(new TVInfo() { Name = "中国教育3_大连理工", Url = " mms://vod2.dlut.edu.cn/CETV3" });
            Tvinfos.Add(new TVInfo() { Name = "BTV1_大连理工", Url = " mms://vod2.dlut.edu.cn/BTV1" });
            Tvinfos.Add(new TVInfo() { Name = "BTV2_大连理工", Url = " mms://vod2.dlut.edu.cn/BTV2" });
            Tvinfos.Add(new TVInfo() { Name = "BTV3_大连理工", Url = " mms://vod2.dlut.edu.cn/BTV3" });
            Tvinfos.Add(new TVInfo() { Name = "BTV4_大连理工", Url = " mms://vod2.dlut.edu.cn/BTV4" });
            Tvinfos.Add(new TVInfo() { Name = "BTV5_大连理工", Url = " mms://vod2.dlut.edu.cn/BTV5" });
            Tvinfos.Add(new TVInfo() { Name = "BTV6_大连理工", Url = " mms://vod2.dlut.edu.cn/BTV6" });
            Tvinfos.Add(new TVInfo() { Name = "BTV7_大连理工", Url = " mms://vod2.dlut.edu.cn/BTV7" });
            Tvinfos.Add(new TVInfo() { Name = "BTV8_大连理工", Url = " mms://vod2.dlut.edu.cn/BTV8" });
            Tvinfos.Add(new TVInfo() { Name = "凤凰中文_大连理工", Url = " mms://vod2.dlut.edu.cn/FHWS" });
            Tvinfos.Add(new TVInfo() { Name = "凤凰资讯_大连理工", Url = " mms://vod2.dlut.edu.cn/FHZX" });
            Tvinfos.Add(new TVInfo() { Name = "香港电影_大连理工", Url = " mms://vod2.dlut.edu.cn/HMC1" });





















            //地方高清

            Tvinfos.Add(new TVInfo() { Name = "东方电影", Url = " http://[2001:da8:8000:6007::242]:3010" });
            Tvinfos.Add(new TVInfo() { Name = "东视戏剧", Url = " http://[2001:da8:8000:6007::242]:3024" });
            Tvinfos.Add(new TVInfo() { Name = "东视娱乐", Url = " http://[2001:da8:8000:6007::242]:3022" });
            Tvinfos.Add(new TVInfo() { Name = "纪实频道", Url = " http://[2001:da8:8000:6007::242]:3026" });
            Tvinfos.Add(new TVInfo() { Name = "上海第一财经", Url = " http://[2001:da8:8000:6007::242]:3017" });
            Tvinfos.Add(new TVInfo() { Name = "上海教育电视台", Url = " http://[2001:da8:8000:6007::242]:3032" });
            Tvinfos.Add(new TVInfo() { Name = "上海外语频道", Url = " http://[2001:da8:8000:6007::242]:3025" });
            Tvinfos.Add(new TVInfo() { Name = "上视电视剧", Url = " http://[2001:da8:8000:6007::242]:3018" });
            Tvinfos.Add(new TVInfo() { Name = "上视生活时尚", Url = " http://[2001:da8:8000:6007::242]:3016" });
            Tvinfos.Add(new TVInfo() { Name = "上视体育", Url = " http://[2001:da8:8000:6007::242]:3019" });
            Tvinfos.Add(new TVInfo() { Name = "上视新闻综合", Url = " http://[2001:da8:8000:6007::242]:3009" });
            Tvinfos.Add(new TVInfo() { Name = "艺术人文", Url = " http://[2001:da8:8000:6007::242]:3023" });
            Tvinfos.Add(new TVInfo() { Name = "City都市剧场", Url = " http://[2001:da8:8000:6007::241]:3039" });
            Tvinfos.Add(new TVInfo() { Name = "Documetary 全纪实", Url = " http://[2001:da8:8000:6007::243]:3077" });
            Tvinfos.Add(new TVInfo() { Name = "动感体育", Url = " http://[2001:da8:8000:6007::243]:3082" });
            Tvinfos.Add(new TVInfo() { Name = "Max极速汽车", Url = " http://[2001:da8:8000:6007::241]:3036" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV", Url = " http://[2001:da8:8000:6007::241]:3035" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV白金剧场", Url = " http://[2001:da8:8000:6007::241]:3040" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV点播影院", Url = " http://[2001:da8:8000:6007::243]:3100" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV动作影院", Url = " http://[2001:da8:8000:6007::243]:3098" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV海外影院", Url = " http://[2001:da8:8000:6007::243]:3096" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV怀旧影院", Url = " http://[2001:da8:8000:6007::243]:3099" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV节目预告", Url = " http://[2001:da8:8000:6007::242]:3001" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV劲爆足球", Url = " http://[2001:da8:8000:6007::241]:3054" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV经典剧场", Url = " http://[2001:da8:8000:6007::241]:3052" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV立体娱乐", Url = " http://[2001:da8:8000:6007::241]:3053" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV情感影院", Url = " http://[2001:da8:8000:6007::243]:3097" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV生活时尚", Url = " http://[2001:da8:8000:6007::241]:3050" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV首映剧场", Url = " http://[2001:da8:8000:6007::241]:3038" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV五星体育", Url = " http://[2001:da8:8000:6007::241]:3055" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV学习考试", Url = " http://[2001:da8:8000:6007::241]:3049" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV英语教室", Url = " http://[2001:da8:8000:6007::241]:3048" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV娱乐前线", Url = " http://[2001:da8:8000:6007::241]:3037" });
            Tvinfos.Add(new TVInfo() { Name = "SiTV资讯气象", Url = " http://[2001:da8:8000:6007::243]:3089" });
            Tvinfos.Add(new TVInfo() { Name = "第一财经", Url = " http://[2001:da8:8000:6007::242]:3017" });
            Tvinfos.Add(new TVInfo() { Name = "东方ICS", Url = " http://[2001:da8:8000:6007::242]:3025" });
            Tvinfos.Add(new TVInfo() { Name = "动漫秀场", Url = " http://[2001:da8:8000:6007::243]:3075" });
            Tvinfos.Add(new TVInfo() { Name = "法制天地", Url = " http://[2001:da8:8000:6007::241]:3056" });
            Tvinfos.Add(new TVInfo() { Name = "哈哈少儿", Url = " http://[2001:da8:8000:6007::243]:3080" });
            Tvinfos.Add(new TVInfo() { Name = "欢笑剧场", Url = " http://[2001:da8:8000:6007::241]:3041" });
            Tvinfos.Add(new TVInfo() { Name = "金色频道", Url = " http://[2001:da8:8000:6007::241]:3042" });
            Tvinfos.Add(new TVInfo() { Name = "劲爆体育", Url = " http://[2001:da8:8000:6007::241]:3058" });
            Tvinfos.Add(new TVInfo() { Name = "绿叶子", Url = " http://[2001:da8:8000:6007::242]:3032" });
            Tvinfos.Add(new TVInfo() { Name = "魅力音乐", Url = " http://[2001:da8:8000:6007::241]:3064" });
            Tvinfos.Add(new TVInfo() { Name = "七彩戏剧", Url = " http://[2001:da8:8000:6007::241]:3057" });
            Tvinfos.Add(new TVInfo() { Name = "卫生健康", Url = " http://[2001:da8:8000:6007::243]:3076" });
            Tvinfos.Add(new TVInfo() { Name = "炫动卡通", Url = " http://[2001:da8:8000:6007::243]:3081" });
            Tvinfos.Add(new TVInfo() { Name = "游戏风云", Url = " http://[2001:da8:8000:6007::243]:3074" });

            //中农
            //Tvinfos.Add(new TVInfo() { Name = "央视综合_中农", Url = "udp://@225.1.1.41:1234" });










            return Tvinfos;
        }
        public void player(string Name)
        {


            TVMediaPlayer.settings.autoStart = true;
            foreach (TVInfo tvinfo in Tvinfos)
            {
                if (Name == tvinfo.Name)
                {

                    TVMediaPlayer.URL = tvinfo.Url;

                }
            }
            //删除缓存

            DeleteTemp();
        }
        private void listBox_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                player(listBox.SelectedItem.ToString());
            }
        }
        private void TV_Click(object sender, RoutedEventArgs e)
        {
            //湖南卫视
            //TVMediaPlayer.URL = "http://[2001:da8:8000:6007::244]:3128";


            //返回控件名



            string controltype = sender.GetType().ToString();
            if (controltype == "Fluent.Button")
            {
                Fluent.Button button = (Fluent.Button)sender;
                player(button.Name);
                button.Focus();

            }
            if (controltype == "Fluent.MenuItem")
            {
                Fluent.MenuItem menuItem = (Fluent.MenuItem)sender;
                player(menuItem.Name);

            }



        }
        private void SearchText_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchText.Text = "";
            //if (listBox.Items.Count > 0)
            //{
            //    listBox.Items.Clear();
            //}

        }
        private void SearchExpander_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (SearchText.IsFocused == false && listBox.IsFocused == false)
            //{
            //    SearchExpander.IsExpanded = false;
            //}

        }
        private void OnBlueClick(object sender, RoutedEventArgs e)
        {
            ResourceDictionary newDictionary = new ResourceDictionary();
            newDictionary.Source = new Uri("pack://application:,,,/Fluent;component/Themes/Office2010/Black.xaml");
            Application.Current.Resources.MergedDictionaries[0] = newDictionary;


        }
        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                listBox.Items.Clear();
                string InputText = SearchText.Text;
                if (InputText != "")
                {

                    foreach (TVInfo tvinfo in Tvinfos)
                    {
                        if (tvinfo.Name != "BlackApplePlayer"&&!tvinfo.Name.Contains("__") && tvinfo.Name.ToUpper().Contains(InputText.ToUpper()))
                        {
                            listBox.Items.Add(tvinfo.Name);
                        }
                    }
                }
                if (InputText == "")
                {
                    foreach (TVInfo tvinfo in Tvinfos)
                    {
                        if (tvinfo.Name != "BlackApplePlayer" && !tvinfo.Name.Contains("__"))
                        {
                            listBox.Items.Add(tvinfo.Name);
                        }

                    }
                }
            }
            catch (System.Exception ex)
            {

            }


        }
        private void RadioButton_MouseDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton radioButton = (System.Windows.Controls.RadioButton)sender;

            //MessageBox.Show(radioButton.Content.ToString());
            if (radioButton.Content.ToString() == "Office 2010 Silver")
            {
                ResourceDictionary newDictionary = new ResourceDictionary();
                newDictionary.Source = new Uri("pack://application:,,,/Fluent;component/Themes/Office2010/Silver.xaml");
                Application.Current.Resources.MergedDictionaries[0] = newDictionary;
            }

            if (radioButton.Content.ToString() == "Office 2010 Black")
            {
                ResourceDictionary newDictionary = new ResourceDictionary();
                newDictionary.Source = new Uri("pack://application:,,,/Fluent;component/Themes/Office2010/Black.xaml");
                Application.Current.Resources.MergedDictionaries[0] = newDictionary;
            }
            if (radioButton.Content.ToString() == "Office 2010 Blue")
            {
                ResourceDictionary newDictionary = new ResourceDictionary();
                newDictionary.Source = new Uri("pack://application:,,,/Fluent;component/Themes/Office2010/blue.xaml");
                Application.Current.Resources.MergedDictionaries[0] = newDictionary;
            }

            //写入配置文件
            WriteConfig();




        }
        private void RibbonWindow_Closed(object sender, EventArgs e)
        {



            //路径： C:\Documents and Settings\Administrator\Local Settings\Temporary Internet Files\Content.IE5
            //通用路径：C:\Documents and Settings\ +user+\Local Settings\Temporary Internet Files\Content.IE5
            //断开连接

            //继续调用，彻底删除缓存
            player("BlackApplePlayer");
            TVMediaPlayer.Ctlcontrols.stop();
            DeleteTemp();

        }
        private void DeleteTemp()
        {
            string user = Environment.UserName;
            string Path = @"C:\Documents and Settings\" + user + @"\Local Settings\Temporary Internet Files\Content.IE5";
            if (DeleRBtn.IsChecked == true)
            {

                try
                {
                    DirectoryInfo di = new DirectoryInfo(Path);
                    di.Delete(true);
                }
                catch (System.Exception ex)
                {

                }
            }
        }
        private void LoadConfig()
        {
            foreach (Configure config in configs)
            {
                if (config.Key == "ThemeStyle" && config.Value == "Silver")
                {
                    Silver.IsChecked = true;
                }
                if (config.Key == "ThemeStyle" && config.Value == "Blue")
                {
                    Blue.IsChecked = true;
                }
                if (config.Key == "ThemeStyle" && config.Value == "Black")
                {
                    Black.IsChecked = true;
                }
                if (config.Key == "DeleBuffer" && config.Value == "DeleRBtn")
                {
                    DeleRBtn.IsChecked = true;
                }
                if (config.Key == "DeleBuffer" && config.Value == "KeepRBtn")
                {
                    KeepRBtn.IsChecked = true;
                }

            }
        }
        private void WriteConfig()
        {

            //将配置信息写回记录
            //初始化以前更新配置信息
            //获取程序当前运行路径
            string str = System.Environment.CurrentDirectory + "\\configure.ini";
            using (StreamWriter sw = new StreamWriter(str))
            {
                foreach (UIElement element in LayoutRoot.Children)
                {
                    if (element is System.Windows.Controls.RadioButton)
                    {
                        System.Windows.Controls.RadioButton current = ((System.Windows.Controls.RadioButton)element);
                        if (current.IsChecked == true && current.GroupName == "ThemeStyle")
                        {
                            sw.WriteLine("ThemeStyle=" + current.Name);
                            continue;
                        }
                        if (current.IsChecked == true && current.GroupName == "DeleBuffer")
                        {
                            sw.WriteLine("DeleBuffer=" + current.Name);
                            continue;

                        }

                    }
                }
            }

            //File.SetAttributes(str, FileAttributes.Hidden);

        }
        private void SearchText_LostFocus(object sender, RoutedEventArgs e)
        {
            //SearchText.Text = "输入搜索";
        }
        private void DeleRBtn_Checked(object sender, RoutedEventArgs e)
        {
            WriteConfig();
        }
        private void KeepRBtn_Checked(object sender, RoutedEventArgs e)
        {
            WriteConfig();
        }

        private void textBlock18_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "http://www.colfun.com/blackappleplayer.html";
            proc.Start();
            
        }

        private void textBlock18_MouseEnter(object sender, MouseEventArgs e)
        {

        }


    }
}
