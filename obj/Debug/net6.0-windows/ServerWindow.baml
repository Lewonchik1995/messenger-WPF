   M S B A M L   `   `   `  ����D  @Messenger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Messenger	Messenger  5   	       Messenger.ServerWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5	   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5
   	   )localclr-namespace:Messenger     5   	   -   5           MaterialDesignBody#���   5   	     MaterialDesignPaper#���  5   	   $��14(�5   	     � Title$  ServerWindow��5   	   $	��450��6   $	��800��6+   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��5      $��0��6   $��Center=�5      $��Center=�6*   $��16(�5      $��Пользователи:��5      �� 5   
   -   5         ��Name$ 	UsersList��5      $��1��6&   $��0��63   $��2��6C    � 5    
   -   5          �Name$ Frame��5       $��3��6     � NavigationUIVisibility$ Hidden=�60   �� 5!   
   -   5         ��Name$ ShowLogs��5!      $��0��5"      $��1��6   $��3��6*    " MaterialDesignFlatDarkBgButton#��[ 5#       � ToolTip$" MaterialDesignFlatDarkButton��5$      $,��&Посмотреть логи чата��5%      6,   �� 5&   
   -   5        $ MessageListView��5&      $��1��6,   $��1��69   $��3��6I   �� 5'   
   -   5        $
 Send��5'      $��2��5(      $��3��6   $��Bottom=�5)      #��[ 5*      $" MaterialDesignFlatDarkButton��5+      $��45��5,      $��Отправить��5-      6!   �� 5.   
   -   5         ��Name$ Message��5.      $��2��5/      $��2��6   $��Center=�50        MaterialDesignFilledTextBox#��[ 51      	 �� AcceptsReturn$	 False��52      
 �� TextWrapping$
 NoWrap=�53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Сообщение��54      �� 55   
   -   5        $
 Exit��55      $��2��56      $��1��6   $��Bottom=�57      #��[ 58      $" MaterialDesignFlatDarkButton��59      $��45��5:      $��
Выйти��5;      6   5<      5=      