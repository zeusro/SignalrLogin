# SignalrLogin
一个测试页面用于模拟扫码过程

原作者:
http://www.cnblogs.com/zsy/p/5882034.html



## 参考的扫码登录
扫码登录
https://passport.weibo.cn/signin/qrcode/scan?qr=QRID-hk-1AYB0a-qAcnG-Tq4K3GovrUpfRLR0Cyprjb688914&sinainternalbrowser=topnav&showmenu=0



天猫
http://b.mashort.cn/L.1tllI
302
https://login.m.taobao.com/qrcodeCheck.htm?adToken=a737996214ffeb6ab19aa35f36a3192c&lgToken=d8255ff17bf5afdb6000250b2468eabc&spm=a313p.31.1yl.3540511441&short_name=L.1tllI&app=chrome


``` JavaScript
// Start the connection
$.connection.hub.logging = true;
$.connection.hub.start().done(init);

```



