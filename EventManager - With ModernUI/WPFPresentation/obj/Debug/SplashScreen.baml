   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFPresentationWPFPresentation  5   	   "    WPFPresentation.SplashScreen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WPFPresentation     5   	     ? Title$  Tadpole??5   	   $	??325??6   $	??400??6&    ? WindowStartupLocation$ CenterScreen=?62   .??5	      ?  ? ColumnDefinitions 5
   
    ? RowDefinitions 5   
   ?? 5      $??1* ?6   ?? 5      $
??0.5* ?6   ?? 5      $??1* ?6   ?? 5      $
??0.5* ?6   ?? 5      $??1* ?6   ?? 5      $??1* ?6   ?? 5      $
??0.5* ?6   ?? 5      $??1* ?6   .+?5   
   ?? $)??#Welcome! Please log in to continue.??6    ?? TextAlignment$ Center=?6?   $??Center=?6V   $??18(?6q   ?? 5   
   $??Email Address:??6   $??1??6*   $??Bottom=?67   $ Center=?6R   ?? 5   
   -   5         ??Name$ txtEmail??5      $??2??6$   $
??20 5q?61   $??Center=?6?   ?? 5   
   $??	Password:??6   $??3??6%   $??Bottom=?62   $ Center=?6M   G? 5   
   -   5         G?Name$ pwdPassword??5      $??4??6+   $
??20 5q?68   $??Center=?6F   ?? 5   
   -   5         ??Name$ btnLogin??5      $??Log in??6#   $??5??64   $??Center=?6A    ?? 	IsDefault . 6u   ?? 5    
   $??Not a user???6   $ Center=?6'   $??Bottom=?6>   $??6??6Y   ?? 5!   
   -   5        $ btnRegister??5!      $??Register here!??6&   $??7??6?   $	??Top=?6L   $??Center=?6d   $??5q?6?   6?   5"      5#      