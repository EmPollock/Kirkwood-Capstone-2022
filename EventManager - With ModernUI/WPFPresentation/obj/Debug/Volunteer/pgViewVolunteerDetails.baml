   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFPresentationWPFPresentation  5      ,    &WPFPresentation.pgViewVolunteerDetails   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WPFPresentation     5        N? Title$  pgViewVolunteerDetails??5	      .??5      ? .+?5   
   ?? -   5         ??Name$ scrVolunteerDetails??5      $y?Visible=?64   .??5      ?  ? RowDefinitions 5      ?? 5      $??50 ?6$   ?? 5      $??20 ?6$   ?? 5      $	??100 ?6$   ?? 5      $??25 ?6$   ?? 5      $??25 ?6$   ?? 5      $??25 ?6$   ?? 5      $	??150 ?6$   ?? 5      ?? 5      $??25 ?6$   ?? 5      ?? 5      ?? 5      $??40 ?6$   ?? 5      $??20 ?6$   .+?5       ? $??0??6    ? ColumnDefinitions 5!      ?? 5"      ?? 5#      .+?5%      ?? -   5         ??Name$ txtVolunteerName??5%       $??0??6:   $??Volunteer Name??6G      TitleTextBlockStyle#??[  6]   $
??Bold"?6?   $
??10 0q?6?   $??Right=?6?   $??Center=?6?   ?? 5&      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6l   -   5         ??Name$ canStarRating??5&      $??6??64   $??1??6A   $
??Left=?6Q   .+?5'      ??  ?? Left$ 20??6!   
 ?? Top$ 0??62   $??8??6A   $??26??6L   .+?5)      ?? $ 5??6%   $ 20??65   .+?5*   "   F? -   5         F?Name$ pathStarOne??5*   '   ???#808080???#C0C0C0$??3??6\   	 ?? StrokeStartLineCap$	 Round=?6p   
 ?? StrokeEndLineCap$
 Round=?6?    ?? StrokeLineJoin$ Round=?6?   a????        р??     ???? ?ig?? -1    р??    ???^??? ?@x} -1???? ??? ?@KL  -1???j ??? 	6?   5+      ?? 5,      $ 45??6%   $ 20??66   .+?5-   "   F? -   5        $ pathStarTwo??5-   '   ???#808080???#C0C0C0$??3??6\   $	 Round=?6p   $
 Round=?6?   $ Round=?6?   a????        р??     ???? ?ig?? -1    р??    ???^??? ?@x} -1???? ??? ?@KL  -1???j ??? 	6?   5.      ?? 5/      $ 85??6%   $ 20??66   .+?50   "   F? -   5        $ pathStarThree??50   '   ???#808080???#C0C0C0$??3??6^   $	 Round=?6r   $
 Round=?6?   $ Round=?6?   a????        р??     ???? ?ig?? -1    р??    ???^??? ?@x} -1???? ??? ?@KL  -1???j ??? 	6?   51      ?? 52      $	 125??6%   $ 20??67   .+?53   "   F? -   5        $ pathStarFour??53   '   ???#808080???#C0C0C0$??3??6]   $	 Round=?6q   $
 Round=?6?   $ Round=?6?   a????        р??     ???? ?ig?? -1    р??    ???^??? ?@x} -1???? ??? ?@KL  -1???j ??? 	6?   54      ?? 55      $	 165??6%   $ 20??67   .+?56   "   F? -   5        $ pathStarFive??56   '   ???#808080???#C0C0C0$??3??6]   $	 Round=?6q   $
 Round=?6?   $ Round=?6?   a????        р??     ???? ?ig?? -1    р??    ???^??? ?@x} -1???? ??? ?@KL  -1???j ??? 	6?   57      58      59      5:      ?? 5<      -	   5        $ txtAboutVolunnteerName??5<      $??1??6<   $??About Volunteer Name:??6I     BodyTextBlockStyle#??[ 6f   $
??Bold"?6?   $
??10 0q?6?   ?? 5=      -
   5         ??Name$ txtBoxAboutVolunteer??5=      $??2??6:   $??20 10q?6G    ?? 
IsReadOnly . " ?? VerticalScrollBarVisibility$
 Auto=?6h   ? 5?      $??3??6    5A      ?? 5B      $	??120 ?6+   ?? 5C      .+?5E      ?? $??0??6    $??Volunteer Type:??60   $
??Bold"?6G   $??	20 0 0 0 q?6Y   ?? 5F      -   5        $ txtVolunteerType??5F       $??1??6:   $
??Left=?6J   5G      ?? 5I      -   5        $ txtSkillSet??5I      $
??Bold"?61   $??5??6C   $??Skills??6P   #??[ 6^   $
??10 0q?6?   ?? 5J      -   5        $ txtBoxSkillSet??5J      $??6??64   $??20 10q?6A    . $
 Auto=?6b   ? 5L      $??7??6   $y?Visible=?6$    5M      ?? 5N      ?? 5O      ?? 5P      .+?5R      ?? -   5         ??Name$ imgVolunteerImage??5R      $??1??67   $??5q?6G   ?? 5S      -   5         ??Name$ btnBack??5S      $
??Back??6.   $??Right=?6=   $??4q?6Y   ?? 5T      -   5        $ btnNext??5T      $??2??6.   $
??Next??6>   $
??Left=?6M   $??4q?6h   5V      ?? 5X      -   5        $ 
txtReviews??5X      $??8??60   $??Reviews??6=   #??[ 6L   $
??Bold"?6x   $
??10 0q?6?   ?? 5Y      -   5        $ txtNoReviewsYet??5Y      $??9??65   $??No reviews yet...??6B   $y?	Collapsed=?6[   #??[ 6r   $
??20 0q?6?   ?? 5Z      -   5        $ txtBoxReviews??5Z      $??9??63   $??20 10q?6@    . 6O   ?? 5[      -   5        $ txtBoxReviewsSecond??5[      $??10??69   $??20 10q?6G    . 6V   ?? 5\      -   5        $ btnMoreReviews??5\      $??11??64   $??More reviews...??6B   $??Medium"?6\   $
??20 0q?6p   ??6~   Z? 5]      5_      5`      5a      