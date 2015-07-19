public class Bits {

    public static void main(String[] args)
    {
        int i = 0;
        int number = Integer.parseInt(args[0]);
        boolean isValid = number >= 0;
        if (isValid) {

            for (i = 0; number >= 1; i++) {
                number /= 2;
            }
        }
        System.out.println(isValid ? i : "Illegal input");
    }
}