from playsound import playsound
import requests, sys, random

wordlist = "abcdefghijklmnopqrstuvwxyz"
search_end_tone = 'C:\example.mp3'

selected_word = ''.join(random.sample(wordlist, 5))
attempts = 0
found_usernames = 0

start_question = input("Start in continue mode or stop mode? (c/s) ")

if start_question == "c":
    print('')
    max_tries = int(input("How many tries should the bot max out at? "))

    if max_tries > 500:
        print('')
        print("Sorry, max tries has to be less than 500.")
        print('')
        max_tries = int(input("How many tries should the bot max out at?"))

    else:
        print('')
        text_file = open("C:\driver\words.txt","w+")

        for i in range (0, max_tries):
            selected_word = ''.join(random.sample(wordlist, 5))
            attempts = attempts + 1

            if (requests.get('https://auth.roblox.com/v1/usernames/validate?request.username=' + selected_word + '&request.birthday=1999-05-08').json()['code']) == 0:
                found_usernames = found_usernames + 1
                print("Attempt: " + str(attempts) + " | Username: " + selected_word)
                text_file.write(selected_word + "  ")

            else:
                print("Attempt: " + str(attempts) + " | Status: TAKEN" + " | Username: " + selected_word)

    if found_usernames == 0:
        print('')
        print("Sorry, no usernames were found in " + str(max_tries) + " attempts.")
        playsound(search_end_tone)

    else:
        print('')
        print("After " + str(max_tries) + " attempts, " + str(found_usernames) + " username(s) were found:")
        print('')
        text_file.close()
        usernames = open("C:\driver\words.txt","r")
        print(usernames.read())
        text_file.close()
        playsound(search_end_tone)

elif start_question == "s":
    print('')
    max_tries = int(input("How many tries should the bot max out at? "))

    if max_tries > 500:
        print('')
        print("Sorry, max tries has to be less than 500.")
        print('')
        max_tries = int(input("How many tries should the bot max out at?"))

    else:
        print('')

        for i in range(0, max_tries):
            selected_word = ''.join(random.sample(wordlist, 5))
            attempts = attempts + 1

            if (requests.get('https://auth.roblox.com/v1/usernames/validate?request.username=' + selected_word + '&request.birthday=1999-05-08').json()['code']) == 0:
                print('')
                print("Username found! Username: " + selected_word)
                playsound(search_end_tone)
                sys.exit()

            else:
                print("Attempt: " + str(attempts) + " | Status: TAKEN" + " | Username: " + selected_word)

        if found_usernames == 0:
            print('')
            print("Sorry, no usernames were found in " + str(max_tries) + " attempts.")
            playsound(search_end_tone)

        else:
            print('')
            playsound(search_end_tone)

else:
    print('')
    print("Sorry, answer is either 'continue' or 'stop'.")