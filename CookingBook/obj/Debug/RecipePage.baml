   M S B A M L   `   `   `  ����F  BCookingBook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CookingBookCookingBook  5          CookingBook.RecipePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:CookingBook     5        N� Title$  
RecipePage��5
      .��5      � ��5   
   �� 5       �� ImageSource$+ %/CookingBook;component/Images/bkg.png��6   .+�5   
   �� -   5         ��Name$ RecipeImage��5      $
��Left=�5      $	��130��5      $��564,129,0,0q�5      $	��Top=�5      $	��160��5      �� 5   
   -   5         ��Name$ ProductList��5      
������ 
������ $
��Left=�6s   $	��148��6�   $��744,118,0,0q�6�   $	��Top=�6�   $	��155��6�   $��21.333(�6�   $��0q�6�    �� SelectionMode$ Single=�6�   ��6[   �� ��5      �� 5      .R�5      �� $��	0,-4,0,-4q�6!    � 	Resources 5       �� 
TargetType �� Property �� Value��%W   '��       �� 5       " ��6$   .�5!   "   ��  � ��$ 0,0,0,0q�6;   5"      5#      .+�5$      �� 	 ��Name$	 ProductItem��6$   $
��Wrap=�5%   $   $��0,10,0,0q�5&   $   $	��120��5'   $   $��12(�5(   $   
 �� LineStackingStrategy$
 BlockLineHeight=�5)   $    �� 
LineHeight$ 12��5*   $   ��5+   $   �� *products_name+5,      5-      5/      �� 52   
   -   5        $��recipeDescr��52      $��0,0,0,0q�53      
������  �� TextWrapping$
 Wrap=�55      $��571,285,184,107q�6&   $��Disabled=�56      $
��Auto=�57      ��. 58      �� 5;   
   -   5        $	 recipeTitle��5;      $
��Left=�5<      $��	45,84,0,0q�5=      $
��Wrap=�5>      
�������$
��Bold"�5@       �� TextAlignment$ Center=�5A      $	��top=�5B      $��32(�5C      $��	Annabelle)�5D      $��	Underline{�6+   $	��340��5E      $	��143��6    �� 5H   
   -   5        $ printBtnImg��5H      $-��'/CookingBook;component/Images/print.png��5I      $
��Left=�5J      $��60��5K      $��
961,28,0,0q�5L      $	��Top=�5M      $��60��5N      �� 5P   
   -   5         ��Name$ print��5P         NoAnimation#��[  5Q      
������ $��
0, 0, 0, 0q�5S      $
��Left=�5T      $��
958,28,0,0q�5U      $	��Top=�5W      $��60��5X      $��60��5Y      �� 5\   
   -   5        $ 
backBtnImg��5\      $/��)/CookingBook;component/Images/backBtn.png��5]      $
��Left=�5^      $	��106��5_      $��
13,442,0,0q�5`      $	��Top=�5a      $	��110��5b      �� 5d   
   -   5        $ 
backButton��5d      
������ $��0,0,0,0q�5f      $
��Left=�5g      $��
12,440,0,0q�5h      $	��Top=�5i      #��[  5j      $	��106��5l      $	��110��5m      5p      5q      