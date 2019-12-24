def IsPalindrome(n):
    digits = list()
    while (n > 0):
        digits.append(n % 10)
        n = n // 10
    numDigits = len(digits);
    for counter in range(numDigits // 2):
        if (digits[counter] != digits[numDigits-1-counter]):
            return False
    return True

palindromes = list()
for i in range(999,99,-1):
    for j in range(i,99,-1):
        mul  = i * j
        if (IsPalindrome(mul)):
                #print(mul, " is a palindrome")
                palindromes.append(mul)
palindromes = sorted(palindromes, reverse = True)
print (palindromes[0])
 
#for counter in range (200,300):
#    if (IsPalindrome(counter)):
#        print(counter)

x = input("Press Enter")
