using System;

namespace GRS_Task_06 {
    class Program {
        public struct Position {
            public int row;
            public int column;
            public Position(int _row = 0, int col = 0) {
                row = _row;
                column = col;
            }
        }

        enum ID { 
            ADD_DOSSIER = 1,
            REMOVE_DOSSIER,
            SHOW_DOSSIERS,
            FIND_BY_LASTNAME,
            EXIT,
        }

        static void Main(string[] args) {
            string[] fullnames = new string[3];
            string[] states = new string[3];
            fullnames[0] = "Головинский Руслан Сергеевич";
            fullnames[1] = "Баранов Константин Павлович";
            fullnames[2] = "Творун Даниил -----";
            states[0] = "Программист C++";
            states[1] = "Программист";
            states[2] = "Full-stack разработчик";

            const int minWindowWidth = 133;
            Console.WindowWidth = minWindowWidth;

            ID id;
            do {
                id = (ID)ShowMenu();
                switch(id) {
                    case ID.ADD_DOSSIER:
                        AddDossier(ref fullnames, ref states);
                        break;
                    case ID.SHOW_DOSSIERS:
                        ShowDossiers(ref fullnames, ref states);
                        break;
                    case ID.REMOVE_DOSSIER:
                        RemoveDossier(ref fullnames, ref states);
                        break;
                    case ID.FIND_BY_LASTNAME:
                        FindByLastName(ref fullnames, ref states);
                        break;
                }
                if(id != ID.EXIT) {
                    Console.SetCursorPosition(0, fullnames.Length + 10);
                    Console.WriteLine("Нажмите любую клавишу.");
                    Console.ReadKey(true);
                }
            } while (id != ID.EXIT);
        }

        static int ShowMenu() {
            Console.Clear();
            const int menuCount = 5;

            string header = "╔════════════════════════════════╗\n" +
                            "║ Выберите действие:             ║\n" +
                            "╠════════════════════════════════╣";
            string footer = "╠════════════════════════════════╣\n" +
                            "║ Для выбора нажмите Enter       ║\n" +
                            "╚════════════════════════════════╝";
            string open = "║ ";
            string close = " ║";

            string[] menu_titles = new string[menuCount];
            menu_titles[0] = "Добавить досье.               ";
            menu_titles[1] = "Удалить досье.                ";
            menu_titles[2] = "Список существующих досье.    ";
            menu_titles[3] = "Найти по фамилии.             ";
            menu_titles[4] = "Выход.                        ";

            Console.WriteLine(header);
            for(int i = 0; i < menuCount; ++i) {
                Console.Write(open);
                if(i == 0) { 
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write(menu_titles[i]);
                if(i == 0) {
                    Console.ResetColor();
                }
                Console.WriteLine(close);
            }
            Console.WriteLine(footer);

            int selected = 0;
            Console.SetCursorPosition(0, selected + 3);
            ConsoleKeyInfo key;
            do {
                key = Console.ReadKey(true);
                switch(key.Key) {
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(0, selected + 3);
                        Console.Write(open);
                        Console.Write(menu_titles[selected]);
                        Console.Write(close);
                        selected = (selected == 0) ? menuCount - 1: selected - 1;
                        Console.SetCursorPosition(0, selected + 3);
                        Console.Write(open);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(menu_titles[selected]);
                        Console.ResetColor();
                        Console.Write(close);
                        break;

                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(0, selected + 3);
                        Console.Write(open);
                        Console.Write(menu_titles[selected]);
                        Console.Write(close);
                        selected = (selected == menuCount - 1) ? 0 : selected + 1;
                        Console.SetCursorPosition(0, selected + 3);
                        Console.Write(open);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(menu_titles[selected]);
                        Console.ResetColor();
                        Console.Write(close);

                        break;

                    case ConsoleKey.Enter:
                        return ++selected;
                }

            } while(true);

        }

        static void AddDossier(ref string[] fullname, ref string[] state) {
            Console.Clear();

            Console.WriteLine("╔═══════════ Добавление досье ═══════════╗");
            Console.WriteLine("║ Заполните форму:                       ║");
            Console.WriteLine("╟────────────────────────────────────────╢");
            Console.WriteLine("║ * Фамилия:                             ║");
            Console.WriteLine("║ * Имя:                                 ║");
            Console.WriteLine("║ * Отчество:                            ║");
            Console.WriteLine("╟────────────────────────────────────────╢");
            Console.WriteLine("║ * Должность:                           ║");
            Console.WriteLine("╟────────────────────────────────────────╢");
            Console.WriteLine("║ Для перемещения по полям используйте   ║");
            Console.WriteLine("║ клавишу TAB.                           ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Position exitPosition = new Position(13, 1);
            const int fieldCount = 4;

            Position[] positions = new Position[fieldCount];
            positions[0] = new Position(3, 13);
            positions[1] = new Position(4, 9);
            positions[2] = new Position(5, 14);
            positions[3] = new Position(7, 15);

            Console.SetCursorPosition(positions[0].column, positions[0].row);

            int curRowId = 0;

            int[] currentColumns = new int[fieldCount];
            for(int i = 0; i < fieldCount; ++i) { 
                currentColumns[i] = positions[i].column;
            }

            string[] fields = new string[fieldCount];
            for(int i = 0; i < fieldCount; ++i) { 
                fields[i] = "";
            }
            ConsoleKeyInfo sybmol;

            do {
                sybmol = Console.ReadKey(true);
                if(sybmol.Key == ConsoleKey.Enter) {
                    Console.SetCursorPosition(exitPosition.column, exitPosition.row);
                    break;
                }
                if(sybmol.Key == ConsoleKey.Backspace) {
                    if(currentColumns[curRowId] <= positions[curRowId].column) {
                        continue;
                    }

                    --(currentColumns[curRowId]);
                    Console.SetCursorPosition(currentColumns[curRowId], positions[curRowId].row);
                    Console.Write(' ');
                    Console.SetCursorPosition(currentColumns[curRowId], positions[curRowId].row);
                    string cuttedStr = fields[curRowId].Remove(fields[curRowId].Length - 1);
                    fields[curRowId] = cuttedStr;
                    continue;
                }
                if(sybmol.Key == ConsoleKey.Tab) {
                    ++curRowId;
                    if(curRowId >= fieldCount) {
                        curRowId = 0;
                    }
                    Console.SetCursorPosition(currentColumns[curRowId], positions[curRowId].row);
                    continue;
                }

                if(currentColumns[curRowId] == 40) {
                    continue;
                }
                ++(currentColumns[curRowId]);
                Console.Write(sybmol.KeyChar);
                fields[curRowId] += sybmol.KeyChar;
            } while (true);

            string name = "";
            for(int i = 0; i < fieldCount - 1; ++i) {
                name += fields[i] + ((i + 1 == fieldCount - 1) ? "" : " ");
            }

            int oldSize = fullname.Length;
            Array.Resize(ref fullname, oldSize + 1);
            fullname[oldSize] = name;
            Array.Resize(ref state, oldSize + 1);
            state[oldSize] = fields[fieldCount - 1];
        }

        static void ShowDossiers(ref string[] fullname, ref string[] state) {
            const int idBlockSize = 5;
            const int nameBlockSize = 90;
            const int stateBlockSize = 28;
            string line = "║{0}{1} │{2}{3}│{4}{5}║";

            Console.Clear();
            Console.WriteLine("╔══════╤══════════════════════════════════════════════════════════════════════════════════════════╤════════════════════════════╗");
            Console.WriteLine("║  id  │                                   Фамилия Имя Отчество                                   │         Должность          ║");
            Console.WriteLine("╠══════╪══════════════════════════════════════════════════════════════════════════════════════════╪════════════════════════════╣");

            if(fullname.Length > 0) { 
                for(int i = 0; i < fullname.Length; ++i) {
                    int id_space_count = idBlockSize - (i.ToString().Length);
                    string id_spacer = new string(' ', id_space_count);

                    int name_space_count = nameBlockSize - (fullname[i].Length);
                    string name_spacer = new string(' ', name_space_count);

                    int state_space_count = stateBlockSize - (state[i].Length);
                    string state_spacer = new string(' ', state_space_count);
                    Console.WriteLine(line, id_spacer, i + 1, fullname[i], name_spacer, state[i], state_spacer);
                }
            } else { 
                Console.WriteLine("║      │                                   Записи отсутствуют                                     │                            ║");
            }

            Console.WriteLine("╚══════╧══════════════════════════════════════════════════════════════════════════════════════════╧════════════════════════════╝");
        }

        static void RemoveDossier(ref string[] fullname, ref string[] state) {
            Console.Clear();
            if(fullname.Length == 0) {
                MsgBox("Внимание! Досье отсутствуют!", "Добавьте досье в главном|меню.");
                return;
            }

            const int idFieldSize = 5;
            const int nameFieldSize = 39;

            // showing 

            Console.Clear();
            int menuCount = fullname.Length;

            string header = "╔══════════════════════════════════════════════╗\n" +
                            "║ Выберите досье:                              ║\n" +
                            "╠══════╤═══════════════════════════════════════╣\n" +
                            "║  id  │ ФИО                                   ║\n" +
                            "╠══════╪═══════════════════════════════════════╣";

            string footer = "╠══════╧═══════════════════════════════════════╣\n" +
                            "║ Для выбора нажмите Enter.                    ║\n" +
                            "║ Для выхода в главное меню нажмите Esc.       ║\n" +
                            "╚══════════════════════════════════════════════╝";
            string open =  "║";
            string close = "║";

            string[] menu_titles = new string[menuCount];
            for(int i = 0; i < menuCount; ++i) {
                string id_spacer = new string(' ', idFieldSize - (i.ToString().Length));
                string fname_spacer = new string(' ', nameFieldSize - fullname[i].Length - 1);
                menu_titles[i] = string.Format("{0}{1} | {2}{3}", id_spacer, i + 1, fullname[i], fname_spacer);
            }

            Console.WriteLine(header);
            for(int i = 0; i < menuCount; ++i) {
                Console.Write(open);
                if(i == 0) { 
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write(menu_titles[i]);
                if(i == 0) {
                    Console.ResetColor();
                }
                Console.WriteLine(close);
            }
            Console.WriteLine(footer);

            int selected = 0;
            Console.SetCursorPosition(0, selected + 5);
            ConsoleKeyInfo key;
            do {
                key = Console.ReadKey(true);
                switch(key.Key) {
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(0, selected + 5);
                        Console.Write(open);
                        Console.Write(menu_titles[selected]);
                        Console.Write(close);
                        selected = (selected == 0) ? menuCount - 1: selected - 1;
                        Console.SetCursorPosition(0, selected + 5);
                        Console.Write(open);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(menu_titles[selected]);
                        Console.ResetColor();
                        Console.Write(close);
                        break;

                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(0, selected + 5);
                        Console.Write(open);
                        Console.Write(menu_titles[selected]);
                        Console.Write(close);
                        selected = (selected == menuCount - 1) ? 0 : selected + 1;
                        Console.SetCursorPosition(0, selected + 5);
                        Console.Write(open);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(menu_titles[selected]);
                        Console.ResetColor();
                        Console.Write(close);

                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            } while(key.Key != ConsoleKey.Enter);

            Array.Copy(fullname, selected + 1, fullname, selected, fullname.Length - selected - 1);
            Array.Copy(state, selected + 1, state, selected, state.Length - selected - 1);

            Array.Resize(ref fullname, fullname.Length - 1);
            Array.Resize(ref state, state.Length - 1);
        }

        static void FindByLastName(ref string[] fullname, ref string[] state) {
            Console.Clear();
            if(fullname.Length == 0) {
                MsgBox("Внимание!", "В хранилище записи отсутствуют.|Добавьте досье в главном|меню.");
                return;
            }

            const int minSearchColumn = 35;
            const int maxSearchColumn = 130;
            const int nameFieldSize = 90;
            const int stateFieldSize = 39;
            int maxLineCount = fullname.Length;

            string header = "╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                            "║ Введите фамилию в строку поиска:                                                                                                 ║\n" +
                            "╠══════════════════════════════════════════════════════════════════════════════════════════╤═══════════════════════════════════════╣\n" +
                            "║                                           ФИО                                            │               Должность               ║\n" +
                            "╠══════════════════════════════════════════════════════════════════════════════════════════╪═══════════════════════════════════════╣";

            string footer = "╠══════════════════════════════════════════════════════════════════════════════════════════╧═══════════════════════════════════════╣\n" +
                            "║ Для выхода в главное меню нажмите клавишу Esc,                                                                                   ║\n" +
                            "╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝";
            string format = "║ {0}{1} │ {2}{3} ║";

            Console.WriteLine(header);
            for(int i = 0; i < maxLineCount; ++i) {
                string name_spacer = new string(' ', nameFieldSize - fullname[i].Length - 2);
                string state_spacer = new string(' ', stateFieldSize - state[i].Length - 2);
                Console.WriteLine(format, fullname[i], name_spacer, state[i], state_spacer);
            }
            Console.WriteLine(footer);

            Position defaultPosition = new Position(5, 0);

            Position cursorPosition = new Position(1, minSearchColumn);
            Console.SetCursorPosition(cursorPosition.column, cursorPosition.row);
            string search = "";
            ConsoleKeyInfo key;
            do {
                key = Console.ReadKey(true);
                switch(key.Key) {
                    case ConsoleKey.Backspace:
                        if(cursorPosition.column == minSearchColumn) {
                            continue;
                        }
                        --cursorPosition.column;
                        Console.SetCursorPosition(cursorPosition.column, cursorPosition.row);
                        Console.Write(' ');
                        Console.SetCursorPosition(cursorPosition.column, cursorPosition.row);
                        if (search.Length > 0) {
                            search = search.Remove(search.Length - 1);
                        }
                        break;
                    default:
                        if(cursorPosition.column != maxSearchColumn) {
                            ++cursorPosition.column;
                            search += key.KeyChar;
                            Console.Write(key.KeyChar);
                        }
                        break;
                }

                Console.SetCursorPosition(defaultPosition.column, defaultPosition.row);
                int foundPos = 0;
                for(int i = 0; i < maxLineCount; ++i) {
                    int spacePos = fullname[i].IndexOf(' ');
                    if(spacePos == -1) {
                        spacePos = fullname[i].Length;
                    }
                    if(!fullname[i].Substring(0, spacePos).Contains(search)) {
                        continue;
                    }
                    ++foundPos;
                    string name_spacer = new string(' ', nameFieldSize - fullname[i].Length - 2);
                    string state_spacer = new string(' ', stateFieldSize - state[i].Length - 2);
                    Console.WriteLine(format, fullname[i], name_spacer, state[i], state_spacer);
                }
                for(int i = foundPos; i < maxLineCount; ++i) {
                    string name_spacer = new string(' ', nameFieldSize - 3);
                    string state_spacer = new string(' ', stateFieldSize - 3);
                    Console.WriteLine(format, ' ', name_spacer, ' ', state_spacer);
                }
                Console.SetCursorPosition(cursorPosition.column, cursorPosition.row);

            } while (key.Key != ConsoleKey.Escape);
        }

        static void MsgBox(string prompt, string text) {
            int fieldSize = Math.Max(text.Split('|')[0].Length, prompt.Split('|')[0].Length);

            string lines = new string('═', fieldSize);

            string[] prompt_s = prompt.Split('|');
            string[] text_s = text.Split('|');

            Console.WriteLine("╔═{0}═╗", lines);

            for(int i = 0; i < prompt_s.Length; ++i) {
                int space_count = fieldSize - prompt_s[i].Length;
                Console.WriteLine("║ {0}{1} ║", prompt_s[i], new string(' ', space_count));
            }

            Console.WriteLine("╠═{0}═╣", lines);

            for(int i = 0; i < text_s.Length; ++i) {
                int space_count = fieldSize - text_s[i].Length;
                Console.WriteLine("║ {0}{1} ║", text_s[i], new string(' ', space_count));
            }

            Console.WriteLine("╚═{0}═╝", lines);
        }
    }
}
