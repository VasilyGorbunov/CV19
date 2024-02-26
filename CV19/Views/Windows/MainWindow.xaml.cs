using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CV19.Models.Decanat;
using Group = CV19.Models.Decanat.Group;

namespace CV19.Views.Windows;

public partial class MainWindow
{
    public MainWindow() => InitializeComponent();

    private void GroupsCollectionFilter(object sender, FilterEventArgs e)
    {
        if (!(e.Item is Group group)) return;
        if(group.Name is null) return;

        var filter_text = GroupNameFilterText.Text;
        if(filter_text.Length == 0) return;

        if (group.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
        if (group.Description != null && group.Description.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

        e.Accepted = false;

    }

    private void OnGroupsFilterTextChanged(object sender, TextChangedEventArgs e)
    {
        var text_box = (TextBox)sender;
        var collection = (CollectionViewSource)text_box.FindResource("GroupsCollection");
        collection.View.Refresh();
    }
}