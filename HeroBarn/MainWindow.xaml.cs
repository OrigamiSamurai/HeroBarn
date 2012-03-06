using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HeroBarnEngine;

namespace HeroBarn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CustomItemSelectPropertyToEdit_ComboBox.DataContext = new string[] { "Weapon", "Armor", "General Item Powers" };
            var validDieSizes = new string[] { "2", "3", "4", "6", "8", "10", "12", "20" };
            CustomWeaponDamageDiceDisplay_ListBox.DataContext = ItemForge.currentCustomWeaponDamageDice;
            CustomWeaponPowersDisplay_ListBox.DataContext = ItemForge.currentCustomWeaponPowers;
            CustomWeaponDamageDieSize_TextBox.DataContext = Validator.GetValidDieSizes();
            CustomWeaponDamageDieType_ComboBox.DataContext = Validator.GetValidGlobalValues("DamageType");
            CustomWeaponHandsUsedByDefault_ComboBox.DataContext = Validator.GetValidGlobalValues("HandsUsedByDefault");
            CustomWeaponProficiency_ComboBox.DataContext = Validator.GetValidGlobalValues("WeaponProficiency");
            CustomWeaponSizedFor_ComboBox.DataContext = Validator.GetValidGlobalValues("Size");
            CustomWeaponSizedFor_ComboBox.SelectedValue = "Medium";

            CustomArmorType_ComboBox.DataContext = Validator.GetValidGlobalValues("ArmorType");
            CustomArmorProficiency_ComboBox.DataContext = Validator.GetValidGlobalValues("ArmorProficiency");
            CustomArmorSizedFor_ComboBox.DataContext = Validator.GetValidGlobalValues("Size");
            CustomArmorSizedFor_ComboBox.SelectedValue = "Medium";
            CustomArmorIsMetal.DataContext = Validator.GetValidGlobalValues("YesOrNo");
            CustomArmorPowersDisplay_ListBox.DataContext = ItemForge.currentCustomArmorPowers;

            CustomItemPowersDisplay_ListBox.DataContext = ItemForge.currentCustomItemPowers;

            CustomItemBasicAttributes_TextBlock.DataContext = ItemForge.currentCustomItemBasicAttributes;
            CustomItemPropertiesDisplay_ListBox.DataContext = ItemForge.currentCustomItemProperties;


        }

        private void CustomWeaponDamageDieAdd_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemForge itemForge = new ItemForge();
            XElement newDamageDie = itemForge.CreateValidatedXElement(
                                    "DamageDie",
                                    new string[] { "DieSize", "DamageType", "Source", },
                                    new string[] { CustomWeaponDamageDieSize_TextBox.Text, CustomWeaponDamageDieType_ComboBox.SelectedValue.ToString(), CustomWeaponDamageDieSource_TextBox.Text }
                                    );
            ItemForge.currentCustomWeaponDamageDice.Add(newDamageDie);
        }
        private void CustomWeaponDamageDieClear_Button_Click(object sender, RoutedEventArgs e)
        {
            var damageDiceToClear = from elements in ItemForge.currentCustomWeaponDamageDice where elements.Name == "DamageDie" select elements;
            var diceList = damageDiceToClear.ToObservableCollection();
            foreach (XElement die in diceList)
            {
                ItemForge.currentCustomWeaponDamageDice.Remove(die);
            }
        }
        private void CustomWeaponDamageDiceDisplay_ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (CustomWeaponDamageDiceDisplay_ListBox.SelectedIndex != -1 && (e.Key == Key.Delete || e.Key == Key.Back))
            {
                ItemForge.currentCustomWeaponDamageDice.RemoveAt(CustomWeaponDamageDiceDisplay_ListBox.SelectedIndex);
            }
        }

        private void CustomWeaponPowerAdd_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemForge itemForge = new ItemForge();
            XElement newPowerLink = itemForge.CreateValidatedXElement(
                                    "PowerLink",
                                    new string[] { "Name", "Source" },
                                    new string[] { CustomWeaponPowerName_TextBox.Text, CustomWeaponPowerSource_TextBox.Text }
                                    );
            ItemForge.currentCustomWeaponPowers.Add(newPowerLink);
        }
        private void CustomWeaponPowerClear_Button_Click(object sender, RoutedEventArgs e)
        {
            var powersToClear = from elements in ItemForge.currentCustomWeaponPowers where elements.Name == "PowerLink" select elements;
            var powerList = powersToClear.ToObservableCollection();
            foreach (XElement power in powerList)
            {
                ItemForge.currentCustomWeaponPowers.Remove(power);
            }
        }
        private void CustomWeaponPowersDisplay_ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (CustomWeaponPowersDisplay_ListBox.SelectedIndex != -1 && (e.Key == Key.Delete || e.Key == Key.Back))
            {
                ItemForge.currentCustomWeaponPowers.RemoveAt(CustomWeaponPowersDisplay_ListBox.SelectedIndex);
            }
        }

        private void CustomItemAddWeaponProperty_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemForge itemForge = new ItemForge();
            var weaponProperty = itemForge.CreateValidatedXElement(
                                   "Weapon",
                                   new string[] { "Name", "CriticalMultiplier", "CriticalRange", "RangeIncrement", "HandsUsedByDefault", "WeaponProficiency", "SizedFor", "Source" },
                                   new string[] {CustomWeaponName_TextBox.Text, CustomWeaponCriticalMultiplier_TextBox.Text, CustomWeaponCriticalRange_TextBox.Text,
                                                 CustomWeaponRangeIncrement_TextBox.Text, CustomWeaponHandsUsedByDefault_ComboBox.SelectedValue.ToString(), CustomWeaponProficiency_ComboBox.SelectedValue.ToString(),
                                                 CustomWeaponSizedFor_ComboBox.SelectedValue.ToString(),CustomWeaponSource_TextBox.Text}
                                   );
            itemForge.AddValidatedChildrenToXElement(weaponProperty, ItemForge.currentCustomWeaponDamageDice.Concat(ItemForge.currentCustomWeaponPowers).ToArray());
            ItemForge.currentCustomItemProperties.Add(weaponProperty);
        }

        private void CustomArmorPowerAdd_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemForge itemForge = new ItemForge();
            XElement newPowerLink = itemForge.CreateValidatedXElement(
                                    "PowerLink",
                                    new string[] { "Name", "Source" },
                                    new string[] { CustomArmorPowerName_TextBox.Text, CustomArmorPowerSource_TextBox.Text }
                                    );
            ItemForge.currentCustomArmorPowers.Add(newPowerLink);
        }
        private void CustomArmorPowerClear_Button_Click(object sender, RoutedEventArgs e)
        {
            var powersToClear = from elements in ItemForge.currentCustomArmorPowers where elements.Name == "PowerLink" select elements;
            var powerList = powersToClear.ToObservableCollection();
            foreach (XElement power in powerList)
            {
                ItemForge.currentCustomArmorPowers.Remove(power);
            }
        }
        private void CustomArmorPowersDisplay_ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (CustomArmorPowersDisplay_ListBox.SelectedIndex != -1 && (e.Key == Key.Delete || e.Key == Key.Back))
            {
                ItemForge.currentCustomArmorPowers.RemoveAt(CustomArmorPowersDisplay_ListBox.SelectedIndex);
            }
        }

        private void CustomItemAddArmorProperty_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemForge itemForge = new ItemForge();
            var armorProperty = itemForge.CreateValidatedXElement(
                                   "Armor",
                                   new string[] { "Name", "ArmorType", "ArmorProficiency", "ArmorClassBonus", "MaxDexterityBonus", "ArmorCheckPenalty", "ArcaneSpellFailureChance", "SizedFor", "IsMetal", "Source" },
                                   new string[] {CustomArmorName_TextBox.Text, CustomArmorType_ComboBox.SelectedValue.ToString(), CustomArmorProficiency_ComboBox.SelectedValue.ToString(), CustomArmorArmorClassBonus_TextBox.Text,
                                                 CustomArmorMaxDexterityBonus_TextBox.Text, CustomArmorCheckPenalty_TextBox.Text, CustomArmorArcaneSpellFailureChance_TextBox.Text,
                                                 CustomArmorSizedFor_ComboBox.SelectedValue.ToString(), CustomArmorIsMetal.SelectedValue.ToString() , CustomWeaponSource_TextBox.Text}
                                   );
            itemForge.AddValidatedChildrenToXElement(armorProperty, ItemForge.currentCustomArmorPowers.ToArray());
            ItemForge.currentCustomItemProperties.Add(armorProperty);
        }

        private void CustomItemPowerAdd_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemForge itemForge = new ItemForge();
            XElement newPowerLink = itemForge.CreateValidatedXElement(
                                    "PowerLink",
                                    new string[] { "Name", "Source" },
                                    new string[] { CustomItemPowerName_TextBox.Text, CustomItemPowerSource_TextBox.Text }
                                    );
            ItemForge.currentCustomItemPowers.Add(newPowerLink);
        }
        private void CustomItemPowerClear_Button_Click(object sender, RoutedEventArgs e)
        {
            var powersToClear = from elements in ItemForge.currentCustomItemPowers where elements.Name == "PowerLink" select elements;
            var powerList = powersToClear.ToObservableCollection();
            foreach (XElement power in powerList)
            {
                ItemForge.currentCustomItemPowers.Remove(power);
            }
        }
        private void CustomItemPowersDisplay_ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (CustomItemPowersDisplay_ListBox.SelectedIndex != -1 && (e.Key == Key.Delete || e.Key == Key.Back))
            {
                ItemForge.currentCustomItemPowers.RemoveAt(CustomItemPowersDisplay_ListBox.SelectedIndex);
            }
        }

        private void CustomItemAddPowers_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemForge itemForge = new ItemForge();
            foreach (XElement power in ItemForge.currentCustomItemPowers)
            {
                ItemForge.currentCustomItemProperties.Add(power);
            };
        }

        private void CustomItemPropertiesDisplay_ListBox_ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (CustomItemPropertiesDisplay_ListBox.SelectedIndex != -1 && (e.Key == Key.Delete || e.Key == Key.Back))
            {
                ItemForge.currentCustomItemProperties.RemoveAt(CustomItemPropertiesDisplay_ListBox.SelectedIndex);
            }
        }

        private void CustomItemSelectPropertyToEdit_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemForge.currentCustomItemBasicAttributes.xElement.IsEmpty)
            {
                ItemForge itemForge = new ItemForge();
                ItemForge.currentCustomItemBasicAttributes.xElement = itemForge.CreateValidatedXElement(
                    "Item",
                    new string[] { "Name", "Cost", "Weight", "HitPoints", "Hardness", "Source" },
                    new string[] { CustomItemName_TextBox.Text, CustomItemCost_TextBox.Text, CustomItemWeight_TextBox.Text,
                                   CustomItemHitPoints_TextBox.Text, CustomItemHardness_TextBox.Text, CustomItemSource_TextBox.Text }
                    );

            };
            PropertyPanelBorder.Visibility = System.Windows.Visibility.Visible;
            CustomItemPropertiesDisplay_ListBox.Visibility = System.Windows.Visibility.Visible;
            switch (CustomItemSelectPropertyToEdit_ComboBox.SelectedValue.ToString())
            {
                case "Weapon":
                    WeaponPropertyPanel.Visibility = System.Windows.Visibility.Visible;
                    ArmorPropertyPanel.Visibility = System.Windows.Visibility.Collapsed;
                    ItemPowersPanel.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "Armor":
                    WeaponPropertyPanel.Visibility = System.Windows.Visibility.Collapsed;
                    ArmorPropertyPanel.Visibility = System.Windows.Visibility.Visible;
                    ItemPowersPanel.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "General Item Powers":
                    WeaponPropertyPanel.Visibility = System.Windows.Visibility.Collapsed;
                    ArmorPropertyPanel.Visibility = System.Windows.Visibility.Collapsed;
                    ItemPowersPanel.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
        }

        private void CustomArmorType_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomArmorProficiency_ComboBox.SelectedValue = CustomArmorType_ComboBox.SelectedValue;
        }

        private void CustomItemUpdateBasicItemAttributes_Click(object sender, RoutedEventArgs e)
        {

            ItemForge itemForge = new ItemForge();
            ItemForge.currentCustomItemBasicAttributes.xElement = itemForge.CreateValidatedXElement(
                "Item",
                new string[] { "Name", "Cost", "Weight", "HitPoints", "Hardness", "Source" },
                new string[] { CustomItemName_TextBox.Text, CustomItemCost_TextBox.Text, CustomItemWeight_TextBox.Text,
                                   CustomItemHitPoints_TextBox.Text, CustomItemHardness_TextBox.Text, CustomItemSource_TextBox.Text }
                );
        }

        private void CustomItemCreateBaseItem_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemForge itemForge = new ItemForge();
            ItemForge.currentCustomItemBasicAttributes.xElement = itemForge.CreateValidatedXElement(
                "Item",
                new string[] { "Name", "Cost", "Weight", "HitPoints", "Hardness", "Source" },
                new string[] { CustomItemName_TextBox.Text, CustomItemCost_TextBox.Text, CustomItemWeight_TextBox.Text,
                                   CustomItemHitPoints_TextBox.Text, CustomItemHardness_TextBox.Text, CustomItemSource_TextBox.Text }
                );
            CustomItemSelectPropertyToEdit_StackPanel.Visibility = System.Windows.Visibility.Visible;
            CustomItemUpdateBasicItemAttributes.Visibility = System.Windows.Visibility.Visible;
            CustomItemCreateBaseItem_Button.Visibility = System.Windows.Visibility.Collapsed;
            CustomItemBasicAttributes_TextBlock.Visibility = System.Windows.Visibility.Visible;
        }

        private void CustomItemAddToLibrary_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemForge itemForge = new ItemForge();
            var newItem = itemForge.AddValidatedChildrenToXElement(ItemForge.currentCustomItemBasicAttributes.xElement, ItemForge.currentCustomItemProperties.ToArray());
            ItemLibrary itemLibrary = new ItemLibrary();
            itemLibrary.Add(newItem);
            itemLibrary.Save();

            CustomItemSelectPropertyToEdit_StackPanel.Visibility = System.Windows.Visibility.Collapsed;
            CustomItemUpdateBasicItemAttributes.Visibility = System.Windows.Visibility.Collapsed;
            CustomItemCreateBaseItem_Button.Visibility = System.Windows.Visibility.Visible;
            CustomItemBasicAttributes_TextBlock.Visibility = System.Windows.Visibility.Collapsed;

            ItemForge.currentCustomWeaponDamageDice = new ObservableCollection<XElement> { };
            ItemForge.currentCustomWeaponPowers = new ObservableCollection<XElement> { };
            ItemForge.currentCustomArmorPowers = new ObservableCollection<XElement> { };
            ItemForge.currentCustomItemPowers = new ObservableCollection<XElement> { };
            ItemForge.currentCustomItemProperties = new ObservableCollection<XElement> { };
            ItemForge.currentCustomItemBasicAttributes = new ObservableXElement("Item");
        }
    }
}
