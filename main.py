from playsound import playsound
import requests, random, sys

characters = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_"
attempts = 0
digits = int(input("How many digits? "))
print('')
max_tries = int(input("How many tries should the bot max out at? "))

if max_tries > 5000:
    print('')
    print("Sorry, max tries has to be less than 5,000.")
    print('')
    max_tries = int(input("How many tries should the bot max out at?"))
else:
    print('')
    for i in range (0,max_tries):
        random_name = ''.join(random.sample(characters, digits))
        attempts = attempts + 1
        if (requests.get('https://auth.roblox.com/v1/usernames/validate?request.username=' + random_name + '&request.birthday=1999-05-08').json()['code']) == 0:
            print('')
            print("BINGO! Name: " + random_name)
            playsound('C:/driver/bong.mp3')
            sys.exit()
        else:
            print("Attempt: " + str(attempts) + " | Status: TAKEN" + " | Username: " + ''.join(random_name))

    print('')
    print("Sorry, no " + str(digits) + " digit names were available.")
    playsound('C:/driver/amongus.mp3')
