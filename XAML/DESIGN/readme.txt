■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
■ DESIGN
------------------------------------------------------------------------------------
■MaterialDesignのアイコンテスト
WpfAppXaml1

導入：
Nugetから
MaterialDesignインストール
MaterialDesignColorインストール

インストール直後はエラーが出る。
リビルドで解消。
------------------------------------------------------------------------------------
■MahApps.Metro.IconPacksテスト
WpfAppXaml4

Material Design In XAML Toolkit のアイコンより数倍扱える。

導入：
Nugetから
インストール直後はエラーが出る。
リビルドで解消。
------------------------------------------------------------------------------------
■HumbergerMenuテスト1(常に出る)
WpfAppXaml5

導入：
Nugetから
MahApps.Metro、MahApps.Metro.IconPacksの２つが必要
https://yotiky.hatenablog.com/entry/wpf_mahapps#HamburgerMenu

備考：
ツールボックスから置くと名前が xmlns:mahになる。
世間的には xmlns:metroとしているらしい。
 
※ModernWpfUIライブラリはライセンス同意出てくるので×
※SplitViewはUWP用のクラスのためWPFでは不可。
------------------------------------------------------------------------------------
■HumbergerMenuテスト(隠せる)
WpfAppXaml6
WpfAppXaml6_v2
WpfAppXaml6_v3(右ハンバーガー)

Material Design In XAML Toolkit版

------------------------------------------------------------------------------------
■HumbergerMenuテスト(隠せる)
WpfAppXaml7

イベントをコードから発生させる

独自Humberger(IInvokeProviderを利用)
左端が限定
右端は小技が必要か?


