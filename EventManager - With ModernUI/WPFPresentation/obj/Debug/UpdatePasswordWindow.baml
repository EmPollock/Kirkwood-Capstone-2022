   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFPresentationWPFPresentation  5   	   *    $WPFPresentation.UpdatePasswordWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WPFPresentation     5   	     ? Title$  UpdatePassword??5   	   $	??260??6    $	??440??6-    ? WindowStartupLocation$ CenterScreen=?69    ? 
ResizeMode$ NoResize=?6^   .??5	      ?  ? RowDefinitions 5
   
   ?? 5      ?? 5      ?? 5      ?? 5      ?? 5       ? ColumnDefinitions 5   
   ?? 5      $	??200 ?6   ?? 5      .+?5   
   ?? -   5         ??Name$ txtInstructions??5      $??0??6-   $??2??6:   $
??Wrap=?6N    ?? TextAlignment$ Center=?6b   $
??50 5q?6y   $
??Bold"?5      $??Yu Gothic UI)?6&   
Title here5      ?? 5   
   $??1??6   $??0??6   $??Center=?6-   $??Right=?6H   $??10q?6d   $??Yu Gothic UI)?6p   .??6?   Old Password:5      G? 5   
   -   5         G?Name$ pwdOldPassword??5      $??1??6.   $??1??6;   $??Center=?6K   $??Center=?6m   $??	10 5 40 5q?6?   $??1??6?   5!      ?? 5#   
   $??2??6   $??0??6   $??Center=?6-   $??Right=?6H   $??10q?6d   $??Yu Gothic UI)?6p   .??6?   New Password:5%      G? 5'   
   -   5        $ pwdNewPassword??5'      $??2??6.   $??1??6;   $??Center=?6K   $??Center=?6m   $??	10 5 40 5q?6?   $??2??6?   5)      ?? 5+   
   $??3??6   $??0??6   $??Center=?6-   $??Right=?6H   $??10q?6d   $??Yu Gothic UI)?6p   .??6?   Retype Password:5-      G? 5/   
   -   5        $ pwdConfirmPassword??5/      $??3??62   $??1??6?   $??Center=?6O   $??Center=?6q   $??	10 5 40 5q?6?   $??3??6?   51      ? 53   
   $??4??6   $??1??6    54      ?? 55      ?? 56      .+?59      ?? -   5         ??Name$ 	btnCancel??59      $??1??6,   $??Cancel??6<   $??	0 5 40 10q?6M   $??5??6x   ?? 5;      -   5        $ 	btnSubmit??5;      $??0??6,   $??Submit??6<   $??
10 5 20 10q?6M   $??4??6y   5<      5>      5?      