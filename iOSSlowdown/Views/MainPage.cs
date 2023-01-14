using iOSSlowdown.ViewModels;
using System.Collections.ObjectModel;
#if IOS
using UIKit;
#endif

namespace iOSSlowdown.Views;

public partial class MainPage : ContentPage
{
    readonly ObservableCollection<CounterViewModel> _viewModel;

    readonly ObservableCollection<CellCounterInfo> _BloodCells;
    

    public MainPage()
    {
        BindingContext = _viewModel = new ObservableCollection<CounterViewModel>();
        for (int i = 0; i < 12; i++)
            _viewModel.Add(new CounterViewModel(0));

        List<CellCounterInfo> cells = new()
        {
            new CellCounterInfo { Counter = _viewModel[0] },
            new CellCounterInfo { Counter = _viewModel[1] },
            new CellCounterInfo { Counter = _viewModel[2] },
            new CellCounterInfo { Counter = _viewModel[3] },
            new CellCounterInfo { Counter = _viewModel[4] },
            new CellCounterInfo { Counter = _viewModel[5] },
            new CellCounterInfo { Counter = _viewModel[6] },
            new CellCounterInfo { Counter = _viewModel[7] },
            new CellCounterInfo { Counter = _viewModel[8] },
            new CellCounterInfo { Counter = _viewModel[9] },
            new CellCounterInfo { Counter = _viewModel[10] },
            new CellCounterInfo { Counter = _viewModel[11] },
        };

        _BloodCells = GenerateCounterGrid(cells);
        BloodCells = new(_BloodCells);

        InitializeComponent();


        CellCounterInfo SetCounterItem(CellCounterInfo counter, int rowIndex, int colIndex)
        {
            counter.ColumnIndex = colIndex;
            counter.RowIndex = rowIndex;
            return counter;
        }

        ObservableCollection<CellCounterInfo> GenerateCounterGrid(IList<CellCounterInfo> cells)
        {
            return new ObservableCollection<CellCounterInfo>
            {
                SetCounterItem(cells[0], 0, 0),
                SetCounterItem(cells[1], 1, 0),
                SetCounterItem(cells[2], 2, 0),
                SetCounterItem(cells[3], 3, 0),

                SetCounterItem(cells[4], 0, 1),
                SetCounterItem(cells[5], 1, 1),
                SetCounterItem(cells[6], 2, 1),
                SetCounterItem(cells[7], 3, 1),

                SetCounterItem(cells[8], 0, 2),
                SetCounterItem(cells[9], 1, 2),
                SetCounterItem(cells[10], 2, 2),
                SetCounterItem(cells[11], 3, 2)
            };
        }
    }

    public ReadOnlyObservableCollection<CellCounterInfo> BloodCells { get; }
}

public class CellCounterInfo : ViewModel
{
    public CounterViewModel? Counter { get; set; }

    int _RowIndex;

    public int RowIndex
    {
        get => _RowIndex;
        set => Set(ref _RowIndex, value);
    }

    int _ColumnIndex;

    public int ColumnIndex
    {
        get => _ColumnIndex;
        set => Set(ref _ColumnIndex, value);
    }
}
