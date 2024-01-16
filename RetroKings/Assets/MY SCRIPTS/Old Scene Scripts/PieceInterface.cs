namespace Assets
{
	interface PieceInterface
	{
		int GetXCoordinate();
		int GetYCoordinate();

		void MoveToCell(int[] newCoords, CellScript cell);
		void Deselect();
	}
}
