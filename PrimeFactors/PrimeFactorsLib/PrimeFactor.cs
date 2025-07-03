namespace PrimeFactorsLib
{
    public class PrimeFactor
    {
        public string PrimeFactors(int n)
        {
            int old_number = n;
            List<int> factors = new List<int>();

            // Проверяем, сколько раз число делится на 2 (наименьший простой множитель)
            while (n % 2 == 0)
            {
                factors.Add(2);
                n /= 2;
            }

            // Теперь проверяем все нечетные числа от 3 до квадратного корня из числа
            for (int i = 3; i * i <= n; i += 2)
            {
                while (n % i == 0)
                {
                    factors.Add(i);
                    n /= i;
                }
            }

            // Если число осталось больше 2, то оно является простым числом
            if (n > 2)
            {
                factors.Add(n);
            }
            int index = 0;
            int count = factors.Count();
            string  result = "";
            foreach (int i in factors)
            {
                result= result+ i+"x";
            }

            //if (count > 0)
            //{
            //    return $"{old_number}:{result.Remove(result.Length - 1)}";
            //    //Console.WriteLine($"{old_number}:{result.Remove(result.Length-1)}");
            //}
            //else
            //{
            //    return $"{old_number}:{old_number}";
            //    // Console.WriteLine($"{old_number}:{old_number}");

            //}

            return $"{old_number}:{string.Join("x", factors)}";


        }
    }
}
