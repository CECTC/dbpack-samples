from flask import Flask, jsonify, request

import time
import mysql.connector

app = Flask(__name__)

allocate_inventory_sql = "update /*+ XID('{xid}') */ product.inventory set available_qty = available_qty - %s, allocated_qty = allocated_qty + %s where product_sysno = %s and available_qty >= %s;"

def conn():
    retry = 0
    while retry < 3:
        time.sleep(5)
        try:
            c = mysql.connector.connect(
              host="dbpack2",
              port=13307,
              user="dksl",
              password="123456",
              database="product",
              autocommit=True,
            )

            if c.is_connected():
                db_Info = c.get_server_info()
                print("Connected to MySQL Server version ", db_Info)
                return c
        except Exception as e:
            print(e.args)
        retry += 1 
 
connection = conn()
cursor = connection.cursor(prepared=True,)

@app.route('/allocateInventory', methods=['POST'])
def create_so():
    xid = request.headers.get('xid')
    reqs = request.get_json()
    if xid and "req" in reqs:
        for res in reqs["req"]:
            try:
                cursor.execute(allocate_inventory_sql.format(xid=xid), (res["qty"], res["qty"], res["product_sysno"], res["qty"],))
            except Exception as e:
                print(e.args)
        
        return jsonify(dict(success=True, message="success")), 200
        
    return jsonify(dict(success=False, message="failed")), 400

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=3002)