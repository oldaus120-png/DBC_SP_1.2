using DataEntity.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using DataEntity.Models;
using DataEntity.Data;
using DBC_SP.ViewModels; // Důležité: Tady říkáme, kde najít ViewModel

namespace DBC_SP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Propojíme View (okno) s ViewModelem (logikou)
            // Tím se aktivují všechny Bindingy v XAMLu
            DataContext = new MainViewModel();
        }
    }
}