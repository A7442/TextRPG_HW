using System;

namespace c_D
{
    public enum Job
    {
        전사 = 1,
        마법사,
        궁수,
        도적
    }
    public enum Item
    {
        녹슨_검________ = 1,
        낡은_지팡이____,
        낡은_활________,
        녹슨_단검______,
        녹슨_방패______ = 15,

        평범한_검______ = 51,
        평범한_지팡이__,
        평범한_활______,
        평범한_단검____,
        평범한_방패____,

        숙련자의_검____ = 101,
        숙련자의_지팡이,
        숙련자의_활____,
        숙련자의_단검__,
        숙련자의_방패__,

        어떤_검________ = 501,
        어떤_지팡이____,
        어떤_활________,
        어떤_단검______,
        어떤_방패______
    }
    public enum ItemExplain
    {
        녹이_슬어있는_검이다 = 1,
        오래되어_낡은_지팡이다,
        오래되어_낡은_활이다,
        녹이_슬어있는_단검이다,
        녹이_슬어있는_방패다 = 15,

        초보자들이_쓰는_평범한_검이다 = 51,
        초보자들이_쓰는_평범한_지팡이다,
        초보자들이_쓰는_평범한_활이다,
        초보자들이_쓰는_평범한_단검이다,
        초보자들이_쓰는_평범한_방패다,

        숙련자들이_쓰는_날카로운_검이다 = 101,
        숙련자들이_쓰는_고귀한_지팡이다,
        숙련자들이_쓰는_고귀한_활이다,
        숙련자들이_쓰는_날카로운_단검이다,
        숙련자들이_쓰는_든든한_방패다,

        어떤 = 501
    }



    internal class Program
    {
        class Player
        {
            Dictionary<int, string> inven = new Dictionary<int, string>();
            Dictionary<int, string> itemability = new Dictionary<int, string>();
            Dictionary<int, string> itemexplain = new Dictionary<int, string>();
            Dictionary<int, bool> iswear = new Dictionary<int, bool>();

            private int inven_num = 1;
            private string playername;
            private int level;
            private string job;
            private int atk;
            private int def;
            private int maxhp;
            private int hp;
            private int gold;

            public string Playername
            {
                get { return playername; }
                set { playername = value; }
            }
            public int Level
            {
                get { return level; }
                set { level = value; }
            }
            public string Job
            {
                get { return job; }
                set { job = value; }
            }
            public int Atk
            {
                get { return atk; }
                set { atk = value; }
            }
            public int Def
            {
                get { return def; }
                set { def = value; }
            }
            public int MaxHp
            {
                get { return maxhp; }
                set { maxhp = value; }
            }
            public int Hp
            {
                get { return hp; }
                set { hp = value; }
            }
            public int Gold
            {
                get { return gold; }
                set { gold = value; }
            }
            public void ShowStatus()
            {
                Console.WriteLine("\n=============== 상태창 ===============\n");
                Console.WriteLine($"이름\t: {playername}");
                Console.WriteLine($"Lv\t: {level}");
                Console.WriteLine($"Job\t: {Job}");
                Console.WriteLine($"공격력\t: {Atk}");
                Console.WriteLine($"방어력\t: {Def}");
                Console.WriteLine($"체력\t: {Hp}");
                Console.WriteLine($"Gold\t: {gold}");
            }

            public void AddInven(int itemnum)
            {
                iswear.Add(inven_num, false);
                inven.Add(inven_num, $"{(Item)itemnum}");
                if (itemnum % 5 == 0)
                {
                    itemability.Add(inven_num, $"방어력+{itemnum / 5}");
                }
                else if (itemnum / 5 == 0)
                {
                    itemability.Add(inven_num, $"공격력+3");
                }
                else
                {
                    itemability.Add(inven_num, $"공격력+{itemnum / 5}");
                }
                itemexplain.Add(inven_num, $"{(ItemExplain)itemnum}");
                inven_num++;
            }

            public void ShowInven()
            {
                Console.WriteLine("\n=============== 소지 아이템 ============================================\n");
                for (int i = 1; i <= inven.Count; i++)
                {
                    Console.Write($"{i}\t");
                    if (iswear[i] == true)
                    {
                        Console.Write("[E]");
                    }
                    Console.Write($"{inven[i]}\tㅣ");
                    Console.Write($"{itemability[i]}\tㅣ");
                    Console.WriteLine($"{itemexplain[i]}");
                }
                Console.WriteLine("\n========================================================================\n");
            }
            public void WearItem(int num)
            {
                int wearweapon = 0;
                int wearshield = 0;
                int itemnum = (int)Enum.Parse(typeof(Item), inven[num]);
                for (int i = 1; i <= iswear.Count; i++)
                {
                    int itnum = (int)Enum.Parse(typeof(Item), inven[i]);
                    if (itnum % 5 == 0 && iswear[i] == true)
                    {
                        wearshield++;
                    }
                    else if (itnum % 5 != 0 && iswear[i] == true)
                    {
                        wearweapon++;
                    }
                }
                if (iswear[num] == false)
                {
                    if (itemnum % 5 == 0 && wearshield == 0)
                    {
                        def += itemnum / 5;
                        iswear[num] = true;
                    }
                    else if (itemnum / 5 == 0 && wearweapon == 0)
                    {
                        atk += 3;
                        iswear[num] = true;
                    }
                    else if (itemnum % 5 != 0 && wearweapon == 0)
                    {
                        atk += itemnum / 5;
                        iswear[num] = true;
                    }
                    else
                    {
                        Console.WriteLine("해당부위의 아이템을 이미 장비하고 있습니다.");
                        Console.WriteLine("무기와 방패는 각각 1개씩만 착용가능합니다.\n\n");
                    }
                }
                else if (iswear[num] == true)
                {
                    if (itemnum % 5 == 0)
                    {
                        def -= itemnum / 5;
                    }
                    else if (itemnum / 5 == 0)
                    {
                        atk -= 3;
                    }
                    else
                    {
                        atk -= itemnum / 5;
                    }
                    iswear[num] = false;
                }
            }
            public int DicLength()
            {
                return inven.Count;
            }

            public void Rest()
            {
                if (hp == maxhp)
                {
                    Console.WriteLine("이미 체력이 최대입니다");
                }
                else if (hp < maxhp)
                {
                    if (gold >= 20)
                    {
                        hp = maxhp;
                        gold -= 20;
                        Console.WriteLine("휴식하여 Hp가 최대가 되었습니다");
                        Console.WriteLine("20gold를 지불하였습니다");
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다");
                        Console.WriteLine($"현재 보유 골드는 {gold}gold입니다");
                    }
                }
            }
            public void SellmyItem(int num)
            {
                int itemnum = (int)Enum.Parse(typeof(Item), inven[num]);
                if (iswear[num] == false)
                {
                    if (itemnum < 5)
                    {
                        Console.WriteLine($"{(Item)itemnum}을(를) 판매하였습니다");
                        itemnum = 15;
                    }
                    Console.WriteLine($"+{((itemnum / 5) * 50) / 2}gold");
                    inven.Remove(num);
                    itemability.Remove(num);
                    itemexplain.Remove(num);
                    iswear.Remove(num);
                    gold += ((itemnum / 5) * 50) / 2;
                    if (num < inven.Count + 1)
                    {
                        for (int i = 1; i <= inven.Count - num + 1; i++)
                        {
                            inven.Add(num, inven[num + 1]);
                            itemability.Add(num, itemability[num + 1]);
                            itemexplain.Add(num, itemexplain[num + 1]);
                            iswear.Add(num, iswear[num + 1]);
                            inven.Remove(num + 1);
                            itemability.Remove(num + 1);
                            itemexplain.Remove(num + 1);
                            iswear.Remove(num + 1);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("착용 중인 장비는 판매할 수 없습니다");
                }
            }

        }

        class Select
        {
            private bool isgameover;
            public bool IsGameOver
            {
                get { return isgameover; }
                set { isgameover = value; }
            }


            Player player = new Player();
            Store store = new Store();
            public void StartScreen()
            {

                string? playername;
                Console.WriteLine("\n============= 어떤 이야기 =============\n\n");
                Console.WriteLine("1. 새 게임\n");
                Console.WriteLine("2. 불러오기\n");
                Console.WriteLine("3. 설정\n");
                Console.WriteLine("0. 종료하기\n\n");
                while (true)
                {
                    Console.Write("숫자를 입력해 주세요\n>>");
                    if (int.TryParse(Console.ReadLine(), out int playerselect))
                    {
                        switch (playerselect)
                        {
                            case 1:
                                Console.WriteLine("\n\n========================================================================================\n");
                                Console.WriteLine("당신은 죽었습니다");
                                Console.WriteLine("21세기 취업을 준비하던 당신은 면접을 보기위해 집을 나섭니다");
                                Console.WriteLine("어떤 회사에서 성공적으로 면접을 본 당신 곁에 어떤 사람이 다가옵니다");
                                Console.WriteLine("순간적으로 정신을 잃은 당신은 미지의 장소에서 눈을 뜨고 앞에는 어떤 여신이 있습니다\n");
                                Console.WriteLine("어떤 여신 : \"열심히 산 당신에게 또 한번의 삶을 드릴게요\"\n");
                                Console.WriteLine("그렇게 당신은 어떤 나라의 평범한 집에서 새로 태어나고 새로운 이름을 받게되는데...");
                                while (true)
                                {
                                    Console.Write("이름을 입력해주세요 : ");
                                    playername = Console.ReadLine();
                                    if (string.IsNullOrEmpty(playername))
                                    {
                                        Console.WriteLine("잘못된 이름입니다\n");
                                    }
                                    else
                                    {
                                        player.Playername = playername;
                                        break;
                                    }
                                }
                                Console.WriteLine($"\n\n당신의 이름은 {playername}입니다.");
                                Console.WriteLine("당신이 태어난 세상은 어떤 판타지 세계였고 당신은 모험가가 되었습니다\n\n");
                                break;
                            case 2:
                                Console.WriteLine("아직 구현을 못했네요 ^^ 새 게임으로 시작할게요^^");
                                break;
                            case 3:
                                Console.WriteLine("설정 그런건 없습니다 ㅠㅠ 죄송합니다 ㅠㅠ");
                                break;
                            case 0:
                                Console.WriteLine("종료!");
                                break;
                            default:
                                Console.WriteLine("그런 숫자는 선택지에 없습니다\n");
                                break;
                        }
                        if (playerselect >= 1 && playerselect <= 3)
                        {
                            isgameover = false;
                            break;
                        }
                        else if (playerselect == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요\n");
                    }
                }
            }
            public void Job_Select()
            {
                while (true)
                {
                    Console.WriteLine("=========================================================================================\n");
                    Console.WriteLine("직업을 선택해주세요\n\n");
                    Console.WriteLine("1. 전사\n");
                    Console.WriteLine("2. 마법사\n");
                    Console.WriteLine("3. 궁수\n");
                    Console.WriteLine("4. 암살자\n");
                    Console.Write("\n원하는 직업의 숫자를 입력해 주세요\n>>");
                    if (int.TryParse(Console.ReadLine(), out int playerselect))
                    {
                        Job job = (Job)playerselect;
                        player.Job = $"{job}";
                        player.Level = 01;
                        player.Gold = 100;
                        player.AddInven(playerselect);

                        switch (playerselect)
                        {
                            case 1:
                                Console.WriteLine($"{job}를 선택했습니다.\n");
                                player.Atk = 6;
                                player.Def = 10;
                                player.MaxHp = 100;
                                player.Hp = 100;
                                break;
                            case 2:
                                Console.WriteLine($"{job}를 선택했습니다.\n");
                                player.Atk = 9;
                                player.Def = 7;
                                player.MaxHp = 90;
                                player.Hp = 90;
                                break;
                            case 3:
                                Console.WriteLine($"{job}를 선택했습니다.\n");
                                player.Atk = 11;
                                player.Def = 5;
                                player.MaxHp = 70;
                                player.Hp = 70;
                                break;
                            case 4:
                                Console.WriteLine($"{job}를 선택했습니다.\n");
                                player.Atk = 8;
                                player.Def = 6;
                                player.MaxHp = 80;
                                player.Hp = 80;
                                break;
                            default:
                                Console.WriteLine("그런 숫자는 선택지에 없습니다");
                                break;
                        }
                        if (playerselect >= 1 && playerselect <= 4)
                        {
                            player.AddInven(15);
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요");
                    }
                }
            }

            public void PlayerActive()
            {
                while (true)
                {
                    Console.WriteLine("\n=============================================================================================\n");
                    Console.WriteLine("원하는 행동을 선택해주세요\n");
                    Console.WriteLine("1. 상태보기\n");
                    Console.WriteLine("2. 인벤토리\n");
                    Console.WriteLine("3. 상점\n");
                    Console.WriteLine("4. 모험 : +500gold, -20Hp\n\n");
                    Console.WriteLine("5. 휴식하기\n\n");
                    Console.Write("\n>>");
                    if (int.TryParse(Console.ReadLine(), out int playerselect))
                    {
                        switch (playerselect)
                        {
                            case 1:
                                StatusManager();
                                break;
                            case 2:
                                WearManager();
                                break;
                            case 3:
                                if (!store.IsStoreSet)
                                {
                                    store.Setstore();
                                    store.IsStoreSet = true;//모험을 갔을 때, 새로운 마을에 도착하게 되고 이 때, store tier와 isStoreSet 바꿔줄 것이다
                                }
                                store.Showstore();
                                Store_Select();

                                break;
                            case 4:
                                Console.WriteLine("모험");
                                player.Gold += 500;
                                player.Hp -= 20;
                                break;
                            case 5:
                                player.Rest();
                                break;
                            default:
                                Console.WriteLine("그런 숫자는 선택지에 없습니다");
                                break;
                        }
                        if (playerselect >= 1 && playerselect <= 5)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요");
                    }
                }
            }
            public void StatusManager()
            {
                while (true)
                {
                    player.ShowStatus();
                    Console.WriteLine("\n0. 나가기\n");
                    if (int.TryParse(Console.ReadLine(), out int playerselect))
                    {
                        switch (playerselect)
                        {
                            case 0:
                                break;
                            default:
                                Console.WriteLine("그런 숫자는 선택지에 없습니다\n");
                                break;
                        }
                        if (playerselect == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요\n");
                    }
                }
            }
            public void WearManager()
            {
                while (true)
                {
                    player.ShowInven();
                    Console.WriteLine("무엇을 하시겠습니까?\n>>");
                    Console.WriteLine("\n\n1. 장착 관리\n");
                    Console.WriteLine("0. 나가기\n\n");
                    Console.Write("번호를 입력해주세요\n>>");
                    if (int.TryParse(Console.ReadLine(), out int playerselect))
                    {
                        switch (playerselect)
                        {
                            case 1:
                                Item_Select();
                                break;
                            case 0:
                                break;
                            default:
                                Console.WriteLine("그런 숫자는 선택지에 없습니다\n");
                                break;
                        }
                        if (playerselect == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요\n");
                    }
                }
            }
            public void Item_Select()
            {
                player.ShowInven();
                int diclength = player.DicLength();
                while (true)
                {
                    Console.Write("장착하거나 해제할 아이템의 숫자를 입력해 주세요\n");
                    Console.WriteLine("0. 나가기\n>>");
                    string input = Console.ReadLine();
                    int playerselect;
                    if (int.TryParse(input, out playerselect) && playerselect > 0 && playerselect <= diclength)
                    {
                        player.WearItem(playerselect);
                        break;
                    }
                    else if (int.TryParse(input, out playerselect) && playerselect == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요\n");
                    }
                }
            }
            public void Store_Select()
            {
                while (true)
                {
                    Console.WriteLine("무엇을 하시겠습니까?\n>>");
                    Console.WriteLine("\n\n1. 구매한다\n");
                    Console.WriteLine("2. 판매한다\n");
                    Console.WriteLine("0. 나가기\n\n");
                    Console.Write("번호를 입력해주세요\n>>");
                    if (int.TryParse(Console.ReadLine(), out int playerselect))
                    {
                        switch (playerselect)
                        {
                            case 1:
                                Store_Item_Select();
                                break;
                            case 2:
                                Store_Inven_Select();
                                break;
                            case 0:
                                Console.WriteLine("종료!");
                                break;
                            default:
                                Console.WriteLine("그런 숫자는 선택지에 없습니다\n");
                                break;
                        }
                        if (playerselect == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요\n");
                    }
                }
            }
            public void Store_Item_Select()
            {
                store.Showstore();
                int diclength = store.Store_diclength();
                while (true)
                {
                    Console.Write("사고싶은 아이템의 숫자를 입력해 주세요(0. 취소)\n>>");
                    string? playersel = Console.ReadLine();
                    int playerselect;
                    if (int.TryParse(playersel, out playerselect) && playerselect > 0 && playerselect <= diclength)
                    {
                        int itemnum = store.BuyStore(playerselect);
                        if (player.Gold >= (itemnum / 5) * 50)
                        {
                            player.Gold -= (itemnum / 5) * 50;
                            Console.WriteLine($"\n{(Item)itemnum}을(를) 구매하였습니다\n-{(itemnum / 5) * 50}gold\n\n");
                            player.AddInven(itemnum);
                        }
                        else
                        {
                            Console.WriteLine("\n골드가 부족합니다\n\n");
                        }

                        break;
                    }
                    else if (int.TryParse(playersel, out playerselect) && playerselect == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요\n");
                    }
                }
            }
            public void Store_Inven_Select()
            {
                player.ShowInven();
                int diclength = player.DicLength();
                while (true)
                {
                    Console.Write("판매할 아이템의 숫자를 입력해주세요\n판매를 할 경우 50%의 가격으로 판매됩니다>>");
                    Console.WriteLine("0. 나가기\n>>");
                    string input = Console.ReadLine();
                    int playerselect;
                    if (int.TryParse(input, out playerselect) && playerselect > 0 && playerselect <= diclength)
                    {
                        player.SellmyItem(playerselect);
                    }
                    else if (int.TryParse(input, out playerselect) && playerselect == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요\n");
                    }
                }
            }
        }
        class Store
        {
            private int store_tier = 1;
            private bool isStoreSet = false;
            public int Store_Tierl
            {
                get { return store_tier; }
                set { store_tier = value; }
            }
            public bool IsStoreSet
            {
                get { return isStoreSet; }
                set { isStoreSet = value; }
            }

            Dictionary<int, string> store = new Dictionary<int, string>();
            Dictionary<int, string> s_itemability = new Dictionary<int, string>();
            Dictionary<int, string> s_itemexplain = new Dictionary<int, string>();
            Dictionary<int, string> s_itemgold = new Dictionary<int, string>();

            public void Setstore()
            {
                int[] randnum = new int[4];
                Random random = new Random();
                for (int i = 1; i <= 4; i++)
                {
                    switch (i)
                    {
                        case 1:
                            store_tier++;
                            randnum[i - 1] = random.Next(50 * store_tier + 1, 50 * store_tier + 5);
                            store_tier--;
                            break;
                        case 2:
                            randnum[i - 1] = random.Next(50 * store_tier + 1, 50 * store_tier + 5);
                            break;
                        case 3:
                            while (true)
                            {
                                randnum[i - 1] = random.Next(50 * store_tier + 1, 50 * store_tier + 5);
                                if (randnum[i - 2] != randnum[i - 1])
                                {
                                    break;
                                }
                            }
                            break;
                        case 4:
                            randnum[i - 1] = 50 * store_tier + 5;
                            break;
                    }
                    store.Add(i, $"{(Item)randnum[i - 1]}");
                    if (randnum[i - 1] % 5 == 0)
                    {
                        s_itemability.Add(i, $"방어력+{randnum[i - 1] / 5}");
                        s_itemgold.Add(i, $"{(randnum[i - 1] / 5) * 50}");
                    }
                    else
                    {
                        s_itemability.Add(i, $"공격력+{randnum[i - 1] / 5}");
                        s_itemgold.Add(i, $"{(randnum[i - 1] / 5) * 50}");
                    }
                    s_itemexplain.Add(i, $"{(ItemExplain)randnum[i - 1]}");
                }
                if (store_tier > 1)
                {
                    int[] plusdic = new int[(store_tier - 1) * 5];
                    for (int i = 0; i < plusdic.Length; i++)
                    {
                        plusdic[i] = 50 * (store_tier - 1 - i / 5) + 1 + i % 5;
                        store.Add(i + 5, $"{(Item)plusdic[i]}");
                        if (plusdic[i] % 5 == 0)
                        {
                            s_itemability.Add(i + 5, $"방어력+{plusdic[i] / 5}");
                            s_itemgold.Add(i + 5, $"{(plusdic[i] / 5) * 50}");
                        }
                        else
                        {
                            s_itemability.Add(i + 5, $"공격력+{plusdic[i] / 5}");
                            s_itemgold.Add(i + 5, $"{(plusdic[i] / 5) * 50}");
                        }
                        s_itemexplain.Add(i + 5, $"{(ItemExplain)plusdic[i]}");
                    }
                }
            }
            public void Showstore()
            {
                Console.WriteLine("\n================== 상점 ==================================================\n");
                for (int i = 1; i <= store.Count; i++)
                {
                    Console.Write($"{i} ");
                    Console.Write($"{s_itemgold[i]}g\t");
                    Console.Write($"{store[i]}\tㅣ");
                    Console.Write($"{s_itemability[i]}\tㅣ");
                    Console.WriteLine($"{s_itemexplain[i]}");
                }
                Console.WriteLine("\n==========================================================================\n");
            }
            public int Store_diclength()
            {
                return store.Count;
            }
            public int BuyStore(int storenum)
            {
                int itemnum = (int)Enum.Parse(typeof(Item), store[storenum]);
                return itemnum;
            }
        }
        class Adventure()
        {

        }

        static void Main(string[] args)
        {
            Player player = new Player();
            Select select = new Select();
            select.StartScreen();
            select.Job_Select();
            Console.WriteLine("\n================= 모험 시작입니다!! ==================\n");
            while (!select.IsGameOver)
            {
                select.PlayerActive();
            }
        }
    }
}