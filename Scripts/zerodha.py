import sys
from kiteconnect import KiteConnect

api_key = "test_api_key_123"
access_token = "test_access_token_789"

kite = KiteConnect(api_key=api_key)
kite.set_access_token(access_token)

def get_ltp(symbol):
    ltp = kite.ltp([symbol])
    print(ltp[symbol]['last_price'])

if __name__ == "__main__":
    command = sys.argv[1]
    symbol = sys.argv[2]

    if command == "ltp":
        get_ltp(symbol)
