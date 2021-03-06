import math
def IsPrime(n):
    if (n <= 1):
        return False
    for counter in range(2,((int)(math.sqrt(n)))+1):
        if (n % counter) == 0:
            return False
    return True

def GetPrimeList(n):
    primes = list()
    for counter in range(3,n+1):
        if IsPrime(counter):
            primes.append(counter)
            if len(primes) % 100 == 0:
                print( len(primes),"th prime is : ", counter)
    return primes

def PrintStreak(primes, start, count, total):
    #print('count ', count, ' :: ', total, ' = ', primes[start:(start+count)])
    print('count ', count, ' :: ', total , " - mark at ", start)
    

n=1000000
primes = (GetPrimeList(n))

print("Found ", len(primes), " primes")

streaklen = 3
mark = 0

while streaklen <= len(primes):
    streaksum = 0
    for counter in range(streaklen):
        streaksum += primes[counter]

    while True:
        if streaksum > n:
            print("Count ", streaklen, " : no match found")
            PrintStreak(primes, lastmark, laststreaklen, laststreaksum)
            break

        if (streaksum in primes):
            PrintStreak(primes, mark, streaklen, streaksum)
            lastmark = mark
            laststreaklen = streaklen
            laststreaksum = streaksum
            break

        if (mark + streaklen >= len(primes)):
            print("Count ", streaklen, " - no match found")
            PrintStreak(primes, lastmark, laststreaklen, laststreaksum)
            break
        streaksum -= primes[mark]
        mark += 1
        streaksum += primes[mark+streaklen-1]

    streaklen += 2
    mark=0
    

x = input("Press Enter")
