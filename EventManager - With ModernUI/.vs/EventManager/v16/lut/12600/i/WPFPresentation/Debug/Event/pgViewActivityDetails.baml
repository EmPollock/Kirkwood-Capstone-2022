   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:WPFPresentation.EventWPFPresentation.Event  5      1    +WPFPresentation.Event.pgViewActivityDetails   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:WPFPresentation.Event     5        N? Title$  pgViewActivityDetails??5	      .??5      ? -   5        $??0??5       ? ColumnDefinitions 5   
   ?? 5      $	??200 ?6   ?? 5       ? RowDefinitions 5   
   ?? 5      ?? 5      ?? 5      ?? 5      ?? 5      ?? 5      ?? 5      ?? 5      ?? 5      .+?5   
   ?? -   5         ??Name$ lblActivityName??5      $??0??6-   $??0??6:   $??2??6J   $??Center=?6^   $??Center=?5         TitleTextBlockStyle#??[  63   Filler6h   ?? 5   
   -   5        $ lblActivityDescription??5      $??1??64   $??0??6A   $??Right=?6Q   $??Center=?5         BodyTextBlockStyle#??[ 63   $
??10 0q?6_   Activity Description:6?   ?? 5!   
   -   5         ??Name$ txtActivityDescripiton??5!      $??1??62   $??2??6?   $??1??6P   $??
10 5 140 5q?6`   " ?? VerticalScrollBarVisibility$
 Auto=?6t   ?? 5#   
   -   5        $ lblPublicActivity??5#      $??3??6/   $??0??6<   $??Right=?6L   $??Center=?6h   #??[ 5$      $
??10 0q?6D   Public Activity:6d   	? 5%   
   -   5         	?Name$ radYesPublicActivity??5%      $??3??64   $??1??6A   $
??Left=?6Q   $??Center=?6l   $??
10 5 150 5q?5&      $??20(?6.   .??6<   Yes6A   	? 5'   
   -   5        $ radNoPublicActivity??5'      $??3??63   $??1??6@   $
??Left=?6P   $??Center=?6k   $??20(?5(      $??
150 5 10 5q?6(   .??6<   No6@   ?? 5*   
   -   5        $ lblStartTime??5*      $??4??6*   $??0??67   $??Right=?6G   $??Center=?6c   #??[ 5+      $
??10 0q?6D   Activity Time:6b   ?? 5,   
   -	   5        $ txtStartTime??5,      $??4??6(   $??1??65   $??
10 5 150 5q?6E   $
??15 0q?6Y   $
??Left=?6h   $??Center=?6?   ?? 5-   
   -
   5        $ 
txtEndTime??5-      $??4??6&   $??1??63   $??
150 5 10 5q?6C   $
??15 0q?6W   $
??Left=?6f   $??Center=?6?   ?? 5/   
   -   5        $ lblSublocation??5/      $??5??6,   $??0??69   $??Right=?6I   $??Center=?6e   #??[ 50      $
??10 0q?6D   Event Sublocation:6f   ?? 51   
   -   5        $ txtEventSublocation??51      $??5??6/   $??1??6<   $??
10 5 140 5q?6L   ?? 53   
   -   5        $ lblEventDate??53      $??6??6*   $??0??67   $??Right=?6G   $??Center=?6c   #??[ 54      $
??10 0q?6D   Date of Event:6b   ?? 55   
   -   5        $ txtEventDate??55      $??6??6(   $??1??65   $??
10 5 140 5q?6E   ?? 57   
   -   5        $ lblEventName??57      $??7??6*   $??0??67   $??Right=?6G   $??Center=?6c   #??[ 58      $
??10 0q?6D   Event Name:6_   ?? 59   
   -   5        $ txtEventName??59      $??7??6(   $??1??65   $??
10 5 140 5q?6E   5:      5;      