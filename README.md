# dotnet-log4net-test
log4netの挙動を確認する。

## 前提条件

* ビルドは[dotnet-cli][]を使っている。
* インストールおよび使い方は[dotnet-cli][]の[README.md](https://github.com/dotnet/cli/blob/rel/1.0.0/README.md)を参照

## 確認点

* ログローテートのやり方。
* ローテート時に圧縮できるのか。

## 結果

### ログローテートのやり方

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler,log4net" />
  </configSections>

  <log4net>
    <!-- 日付とサイズでRollingするファイルAppenderの例 -->
    <appender name="DaySizeRollingLogToFile" type="log4net.Appender.RollingFileAppender">
      <!-- ログ・ファイル名 -->
      <File value="C:\Work\MyLog.log" />
      <!-- 追加書き込み -->
      <appendToFile value="true" />
      <!-- 日付ごとにファイルを作成することを指定 -->
      <rollingStyle value="Composite" />
      <!-- ログ・ファイル名を固定にするため“true”を指定 -->
      <staticLogFileName value="true" />
      <!-- ファイルサイズの上限 -->
      <maximumFileSize value="10KB" />
      <!-- 最大バックアップファイル数 -->
      <maxSizeRollBackups value="5" />

      <!-- ファイル名の日付部分 -->
      <datePattern value='"."yyyyMMdd".log"' />

      <layout type="log4net.Layout.PatternLayout">
        <!-- ログの書式 -->
        <conversionPattern value="%d[%t] %p - %m%n"/>
      </layout>
    </appender>


    <root>
      <!-- すべてのログを出力したい場合 -->

      <level value="All" />
      <!-- どのログ出力先を使用するか -->
      <appender-ref ref="DaySizeRollingLogToFile" />
    </root>
  </log4net>
</configuration>
```

#### 参考サイト

* [Wait Cursor - Log4Net設定　MAXファイルサイズ及び日単位で切替え](https://waitcursor.wordpress.com/2013/05/27/log4net%E8%A8%AD%E5%AE%9A%E3%80%80max%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%E3%82%B5%E3%82%A4%E3%82%BA%E5%8F%8A%E3%81%B3%E6%97%A5%E5%8D%98%E4%BD%8D%E3%81%A7%E5%88%87%E6%9B%BF%E3%81%88/)

### ローテート時に圧縮できるか

結論 => **できない**

[ソース](https://github.com/apache/log4net/blob/trunk/src/Appender/RollingFileAppender.cs)からそんな機能があるか確認。
おそらくなさそう。

ちなみになぜできるかと思ったかというと。
[log4jだとできる](http://www.nurs.or.jp/~sug/soft/log4j/log4j5.htm#sec7)から。

[dotnet-cli]:https://github.com/dotnet/cli

### [追記] ローテートの動きについて

日付＆容量でのローテートができることを確認したが、最大バックアップファイル数が効くのは **同一ファイル名のみ** ということがわかった。。。

なので、もしハウスキープを一括で実施したい場合は、ファイル名に日付を付けず、ファイルサイズローテとする必要があるorz

日付ローテでのハウスキープはできない様子。。。

#### 参考サイト

* [shima111の日記 - ログ！ログ！ログ！！](http://d.hatena.ne.jp/shima111/20060629/p1) [調査日:2016/02/07] [掲載日:2006/06/29]