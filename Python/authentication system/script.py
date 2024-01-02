# The Modules for Sending Gmail
from email.message import EmailMessage
import ssl
import smtplib 
#  The Modules for Random Number
from random import randint
# The Module for function decorator 
from functools import wraps
from uu import encode
# The Module for connection to mysql
from mysql.connector import connect
# The Module for timer
from time import sleep
# This is For hashing passwords
from hashlib import sha256

# connect to database
db = connect(
  host = "localhost",
  user = "root",
  password = "12345687",
  database = "py_shop"
)
cursor_db = db.cursor()

# information for sending Email
email_sender = 'empire.boy3000@gmail.com'
email_pass = "auvsitptjvxfrrbq"
email_receiver = ""



verify_code = "" # verfi code for sign in
# loop for creating verify code
for _ in range(6):
    verify_code += chr(randint(66, 90))
    
# decorator for checking is username is has in db 
def check_username(func):
    @wraps(func)
    def inner(*args):
        cursor_db.execute(f"SELECT * from users WHERE user_name = '{args[0]}'")
        result = cursor_db.fetchall()
        if len(result) == 0:
            return func(*args)
        else:
            print("This UserName is Invalid!")
    return inner


# the functoin for sign in user
@check_username
def sign_in(user_name, user_password, user_email):
    # Checking the password is has 8 charecter
    if len(user_password) < 8:
        print("Please Enter valid password.")
        sign_in(user_name, input("Please Enter password Again : "), user_email)
    if not(user_email.endswith("@gmail.com")):
        sign_in(user_name, user_password, input("Please Enter Again Email : "))
    global email_receiver
    email_receiver = user_email

    # Message of Email
    subject = "verification code for signin"
    body = f"""
    verification Code : {verify_code} 
    """
    
    # Email Items
    em = EmailMessage()
    em['From'] = email_sender
    em['To'] = email_receiver
    em['Subject'] = subject
    em.set_content(body)
    
    context = ssl.create_default_context()
    
    flag = False
    # Login to main gmail and send email to receiver
    try:
        with smtplib.SMTP_SSL('smtp.gmail.com', 465, context=context) as smtp:
            smtp.login("empire.boy3000@gmail.com", email_pass)
            smtp.sendmail(email_sender, email_receiver, em.as_string())
            print("We Send Email Please Check Out")
            flag = True
            
    except:
        print("We Can't Send Email!") # Alert the warning
        print("Happend A Problem")
    if flag:
        verification_code = input("Please Enter Verification Code (time = 5m) : ") # receive the verify code we send
        
        # Timer for verify code remaining
        time = 300
        while time > 0:
            time -= 1
            sleep(1)
            if verification_code == verify_code:
                break
            
        if verification_code == verify_code and time != 0: # check conditions and add user to db
            cursor_db.execute(f"INSERT INTO users(user_name, user_password, user_email, user_type) VALUES('{user_name}', '{sha256(user_password).hexdigest()}', '{user_email}', 'user')")
            db.commit()
            print("Sign is Succesfull")
        else:
            print("We can't sing you")

# Login function for users
def login(user_name, password):
    cursor_db.execute(f"SELECT * FROM users WHERE user_name = '{user_name}' AND user_password = '{sha256(password).hexdigest()}'")
    if len((user_found := cursor_db.fetchall())) > 0: # check user is valid
        print(f'Welcome {user_found[0][0]}')
    else:
        print("We can't found this username with this password")

        
# displaying what user do 
choose = int(input("""
                   
1.Log In
2.Sign In
                   
Choose an Option : """))

# Log in Option inputs and function
if choose == 1:
    user_name_value = input("username : ")
    user_pass_value = input("password : ").encode('utf-8')
    login(user_name_value, user_pass_value)
# Sign in Option inputs and function
if choose == 2:
    user_name_value = input("username : ")
    user_pass_value = input("password : ").encode('utf-8')
    user_email_value = input("email : ")
    sign_in(user_name_value, user_pass_value, user_email_value)

