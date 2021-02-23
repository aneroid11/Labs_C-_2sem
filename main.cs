using System;
using System.Collections.Generic;

namespace Tictactoe {
	class Game {
		private const int NUM_SQUARES = 9;
    private const char EMPTY = ' ';
    private const char DRAW = 'D';
    private const char NO_ONE = ' ';

    public static void PrintInstructions() {
      System.Console.WriteLine("Welcome to the game. To declare your move, you" +
									 " should enter a number [1 - 9], like so: ");
			System.Console.WriteLine("\t1 | 2 | 3");
			System.Console.WriteLine("\t---------");
			System.Console.WriteLine("\t4 | 5 | 6");
			System.Console.WriteLine("\t---------");
			System.Console.WriteLine("\t7 | 8 | 9\n");
			System.Console.WriteLine("Let's begin.\n\n\n");
		}

		public static char GetMoveOrder() {
			System.Console.WriteLine("Do you want to move first? (enter y/n)");
			ConsoleKeyInfo yn = System.Console.ReadKey();

			while (yn.Key != ConsoleKey.Y && yn.Key != ConsoleKey.N) {
				System.Console.WriteLine("\nYou entered neither y nor n. Please enter the key again");
				yn = System.Console.ReadKey();
			}

			if (yn.Key == ConsoleKey.Y) {
				return 'X';
			}
			else if (yn.Key == ConsoleKey.N) {
				return '0';
			}

			return ' ';
		}

		public static void DisplayBoard(char[] board) {
			System.Console.WriteLine("\t1 | 2 | 3");
			System.Console.WriteLine("\t---------");
			System.Console.WriteLine("\t4 | 5 | 6");
			System.Console.WriteLine("\t---------");
			System.Console.WriteLine("\t7 | 8 | 9\n\n\n");

			System.Console.WriteLine("\n\t" + board[0] + " | " + board[1] + " | " + board[2]);
			System.Console.WriteLine("\t---------");
			System.Console.WriteLine("\t"   + board[3] + " | " + board[4] + " | " + board[5]);
			System.Console.WriteLine("\t---------");
			System.Console.WriteLine("\t"   + board[6] + " | " + board[7] + " | " + board[8]);
			System.Console.WriteLine("\n\n");
		}

		public static char GetWinner(char[] board) {
			//checking diagonals
			if ( board[0] == board[4] && board[4] == board[8] && board[0] != EMPTY ) { return board[0]; }
			if ( board[2] == board[4] && board[4] == board[6] && board[2] != EMPTY ) { return board[2]; }

			//checking columns
			for (int x = 0; x < 3; x++) {
				if (board[x] == board[3 + x] && board[3 + x] == board[6 + x] && board[x]  != ' ') {
					return board[x];
				}
			}

			//checking rows
			for (int y = 0; y < 3; y++) {
				if ( board[y * 3] == board[y * 3 + 1] && board[y * 3 + 1] == board[y * 3 + 2] && board[y * 3] != ' ') {
					return board[ y * 3 ];
				}
			}

			int emptyCells = 0;

			for (int i = 0; i < board.Length; i++) {
				if (board[i] == EMPTY) { emptyCells++; }
			}

			if (emptyCells == 0) { return DRAW; }

			return NO_ONE;
		}

		public static char Opponent(char pl) {
			if (pl == 'X') { return '0'; }

			return 'X';
		}

		public static bool IsOccupied(int move, char[] board) {
			return board[move] != EMPTY;
		}

		public static int GetNumber1to9() {
			System.Console.WriteLine("Enter a number (1 - 9): ");
			ConsoleKeyInfo a = System.Console.ReadKey();

			while (a.Key < ConsoleKey.D1 || a.Key > ConsoleKey.D9) {
				System.Console.WriteLine("\nPlease repeat the input. Enter a number from 1 to 9");
				a = System.Console.ReadKey();
			}

			int num = a.Key - ConsoleKey.D1;

			return num;
		}

		public static int GetHumanMove(char[] board) {
			int move = GetNumber1to9();

			while (IsOccupied(move, board)) {
				System.Console.WriteLine("\nThe cell is already occupied. Please repeat the input");
				move = GetNumber1to9();
			}

			return move;
		}

		public static void Swap(ref int a1, ref int a2) {
			int temp = a1;
			a1 = a2;
			a2 = temp;
		}

		public static int GetResultOfMove(int move, char turn, char[] board) {
			char[] boardCopy = new char[9];

			for (int i = 0; i < 9; i++) {
				boardCopy[i] = board[i];
			}

			boardCopy[move] = turn;
			turn = Opponent(turn);

			if (GetWinner(boardCopy) == NO_ONE) {
				List<int> possibleMoves = new List<int>();

				for (int i = 0; i < 9; i++) {
					if (IsOccupied(i, boardCopy)) { continue; }

					possibleMoves.Add(i);
				}

				int[] movesResults = new int[possibleMoves.Count];

				for (int i = 0; i < possibleMoves.Count; i++) {
					movesResults[i] = GetResultOfMove(possibleMoves[i], turn, boardCopy);
				}

				int iRet = 0;

				for (int i = 1; i < possibleMoves.Count; i++) {
					if (turn == 'X') {
						if (movesResults[iRet] > movesResults[i]) {
							Swap(ref iRet, ref i);
						}
					} else {
						if (movesResults[iRet] < movesResults[i]) {
							Swap(ref iRet, ref i);
						}
					}
				}

				return movesResults[iRet];
			} else {
				if (GetWinner(boardCopy) == 'X') {
					return -1;
				} else if (GetWinner(boardCopy) == '0') {
					return 1;
				} else {
					return 0;
				}
			}
		}

		public static int GetComputerMove(char[] board, char computer) {
			int move;
			List<int> possibleMoves = new List<int>();

			for (int i = 0; i < 9; i++) {
				if (IsOccupied(i, board) ) { continue; }

				possibleMoves.Add(i);
			}

			int[] movesResults = new int[possibleMoves.Count];

			for (int i = 0; i < possibleMoves.Count; i++) {
				movesResults[i] = GetResultOfMove(possibleMoves[i], computer, board);
			}

			int iBestMove = 0;

			for (int i = 1; i < possibleMoves.Count; i++) {
				if (computer == 'X') {
					if (movesResults[iBestMove] > movesResults[i]) {
						Swap(ref iBestMove, ref i);
					}
				} else {
					if (movesResults[iBestMove] < movesResults[i]) {
						Swap(ref iBestMove, ref i);
					}
				}
			}

			Random rand = new Random();

			//creating mistakes
			if (rand.Next(6) == 0) {
				move = possibleMoves[0];
			}
			else {
				move = possibleMoves[iBestMove];
			}

			return move;
		}

		public static void PrintWinner(char winner, char human, char computer) {
			if ( winner == human ) {
				System.Console.WriteLine("You won");
			} else if ( winner == computer ) {
				System.Console.WriteLine("You lost");
			} else {
				System.Console.WriteLine("Draw");
			}
		}

		public static void Main(string[] args) {
			PrintInstructions();

			char human = GetMoveOrder();
			char computer = Opponent(human);

			char[] board = new char[NUM_SQUARES];

			for (int i = 0; i < NUM_SQUARES; i++) {
				board[i] = EMPTY;
			}

			System.Console.Clear();
			DisplayBoard(board);

			char turn = 'X';

			while (GetWinner(board) == NO_ONE) {
				if (turn == human ) {
					int move = GetHumanMove(board);
					board[move] = human;
				} else {
					int move = GetComputerMove(board, computer);
					board[move] = computer;
				}

				turn = Opponent(turn);
				System.Console.Clear();
				DisplayBoard(board);
			}

			PrintWinner(GetWinner(board), human, computer);
		}
	}
}
