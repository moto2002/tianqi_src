using System;

public interface ListViewInterface
{
	Cell CellForRow(ListView listView, int row);

	float SpacingForRow(ListView listView, int row);

	uint CountOfRows(ListView listView);
}
