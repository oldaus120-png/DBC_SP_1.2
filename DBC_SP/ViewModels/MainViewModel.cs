using DataEntity;    // Kontext databáze
using DataEntity.Data;
using DataEntity.Models;
using DBC_SP.Core;   // Aby fungoval RelayCommand (pokud ho máte ve složce Core)
using DBC_SP.Views;  // Aby fungovala okna (pokud jste je přesunul do Views)
using Microsoft.EntityFrameworkCore;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace DBC_SP.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly InventarDbContext _db = new();

        public ObservableCollection<Notebook> Notebooky { get; } = new();
        public ObservableCollection<Objednavka> Objednavky { get; } = new();
        public ObservableCollection<Uzivatel> Uzivatele { get; } = new();

        private Notebook? _vybranyNotebook;
        public Notebook? VybranyNotebook
        {
            get => _vybranyNotebook;
            set { _vybranyNotebook = value; OnPropertyChanged(); }
        }

        private Uzivatel? _vybranyUzivatel;
        public Uzivatel? VybranyUzivatel
        {
            get => _vybranyUzivatel;
            set { _vybranyUzivatel = value; OnPropertyChanged(); }
        }

        private Objednavka? _vybranaObjednavka;
        public Objednavka? VybranaObjednavka
        {
            get => _vybranaObjednavka;
            set { _vybranaObjednavka = value; OnPropertyChanged(); }
        }

        // Příkazy
        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }
        public ICommand DeleteAllCommand { get; }
        public ICommand AddNotebookCommand { get; }
        public ICommand DeleteNotebookCommand { get; }
        public ICommand AddUzivatelCommand { get; }
        public ICommand DeleteUzivatelCommand { get; }
        public ICommand AddObjednavkaCommand { get; }
        public ICommand DeleteObjednavkaCommand { get; }

        public MainViewModel()
        {
            SaveCommand = new RelayCommand(SaveData);
            ReloadCommand = new RelayCommand(ReloadData);
            DeleteAllCommand = new RelayCommand(DeleteAllData);
            AddNotebookCommand = new RelayCommand(AddNotebook);
            DeleteNotebookCommand = new RelayCommand(DeleteNotebook);
            AddUzivatelCommand = new RelayCommand(AddUzivatel);
            DeleteUzivatelCommand = new RelayCommand(DeleteUzivatel);
            AddObjednavkaCommand = new RelayCommand(AddObjednavka);
            DeleteObjednavkaCommand = new RelayCommand(DeleteObjednavka);

            // Kontrola design módu
            if (Application.Current != null && !DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                NactiVse();
            }
        }

        private void NactiVse()
        {
            Notebooky.Clear(); Objednavky.Clear(); Uzivatele.Clear();
            try
            {
                foreach (var x in _db.Notebooky.ToList()) Notebooky.Add(x);
                foreach (var x in _db.Uzivatele.ToList()) Uzivatele.Add(x);
                foreach (var x in _db.Objednavky.Include(o => o.Uzivatel).Include(o => o.Notebook).ToList())
                    Objednavky.Add(x);
            }
            catch { }
        }

        // --- PŘIDÁNÍ NOTEBOOKU ---
        private void AddNotebook(object? parameter)
        {
            var novy = new Notebook
            {
                IDNotebooku = Random.Shared.Next(100000, 999999),
                Detail = "Nové",
                Model = "",
                Vyrobce = "",
                CPU = ""
            };

            var okno = new NotebookWindow();
            okno.DataContext = novy;

            if (okno.ShowDialog() == true)
            {
                if (string.IsNullOrEmpty(novy.Model)) { MessageBox.Show("Chybí model!"); return; }
                _db.Notebooky.Add(novy);
                Notebooky.Add(novy);
                VybranyNotebook = novy;
            }
        }

        // --- PŘIDÁNÍ UŽIVATELE ---
        private void AddUzivatel(object? parameter)
        {
            var novy = new Uzivatel
            {
                IDUzivatele = Random.Shared.Next(100000, 999999),
                Jmeno = "",
                Prijmeni = "",
                Email = "",
                Telefon = "",
                Adresa = ""
            };

            var okno = new UzivatelWindow(); // Zkontrolujte, zda máte UzivatelWindow ve Views
            okno.DataContext = novy;

            if (okno.ShowDialog() == true)
            {
                if (string.IsNullOrEmpty(novy.Prijmeni)) { MessageBox.Show("Chybí příjmení!"); return; }
                _db.Uzivatele.Add(novy);
                Uzivatele.Add(novy);
                VybranyUzivatel = novy;
            }
        }

        // --- PŘIDÁNÍ OBJEDNÁVKY ---
        private void AddObjednavka(object? parameter)
        {
            if (Uzivatele.Count == 0 || Notebooky.Count == 0)
            {
                MessageBox.Show("Nejdříve musíte mít vytvořené uživatele a notebooky!");
                return;
            }

            var novaObjednavka = new Objednavka
            {
                IDObjednavky = Random.Shared.Next(100000, 999999),
                DatumObjdenavky = DateTime.Now
            };

            var editorVM = new ObjednavkaEditorViewModel
            {
                VybranaObjednavka = novaObjednavka,
                SeznamUzivatelu = Uzivatele.ToList(),
                SeznamNotebooku = Notebooky.ToList()
            };

            var okno = new ObjednavkaWindow();
            okno.DataContext = editorVM;

            if (okno.ShowDialog() == true)
            {
                if (novaObjednavka.Uzivatel == null || novaObjednavka.Notebook == null)
                {
                    MessageBox.Show("Musíte vybrat zákazníka a notebook!");
                    return;
                }

                novaObjednavka.CenaBezDPH = novaObjednavka.Notebook.Cena;
                novaObjednavka.CenaSDPH = novaObjednavka.Notebook.Cena * 1.21m;
                novaObjednavka.IDUzivatele = novaObjednavka.Uzivatel.IDUzivatele;
                novaObjednavka.IDNotebooku = novaObjednavka.Notebook.IDNotebooku;

                _db.Objednavky.Add(novaObjednavka);
                Objednavky.Add(novaObjednavka);
                VybranaObjednavka = novaObjednavka;
            }
        }

        private void DeleteNotebook(object? p)
        {
            if (VybranyNotebook != null && MessageBox.Show("Smazat?", "Smazat", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try { _db.Notebooky.Remove(VybranyNotebook); Notebooky.Remove(VybranyNotebook); } catch { MessageBox.Show("Nelze smazat."); }
            }
        }
        private void DeleteUzivatel(object? p)
        {
            if (VybranyUzivatel != null && MessageBox.Show("Smazat?", "Smazat", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try { _db.Uzivatele.Remove(VybranyUzivatel); Uzivatele.Remove(VybranyUzivatel); } catch { MessageBox.Show("Nelze smazat."); }
            }
        }
        private void DeleteObjednavka(object? p)
        {
            if (VybranaObjednavka != null && MessageBox.Show("Smazat?", "Smazat", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _db.Objednavky.Remove(VybranaObjednavka); Objednavky.Remove(VybranaObjednavka);
            }
        }
        private void SaveData(object? p) { try { _db.SaveChanges(); MessageBox.Show("Uloženo"); } catch { MessageBox.Show("Chyba."); } }
        private void ReloadData(object? p) { foreach (var e in _db.ChangeTracker.Entries()) e.Reload(); NactiVse(); }
        private void DeleteAllData(object? p)
        {
            if (MessageBox.Show("Smazat vše?", "Pozor", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _db.ChangeTracker.Clear();
                    _db.Objednavky.RemoveRange(_db.Objednavky); _db.SaveChanges();
                    _db.Notebooky.RemoveRange(_db.Notebooky); _db.Uzivatele.RemoveRange(_db.Uzivatele); _db.SaveChanges();
                    Notebooky.Clear(); Uzivatele.Clear(); Objednavky.Clear();
                }
                catch { MessageBox.Show("Chyba mazání."); }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class ObjednavkaEditorViewModel
    {
        public Objednavka VybranaObjednavka { get; set; } = null!;
        public List<Uzivatel> SeznamUzivatelu { get; set; } = new();
        public List<Notebook> SeznamNotebooku { get; set; } = new();
    }
}