import 'package:flutter/material.dart';
import 'package:flutter_users/services.dart';

class UserProfile extends StatefulWidget {
  const UserProfile({Key? key}) : super(key: key);

  @override
  State<UserProfile> createState() => _UserProfileState();
}

class _UserProfileState extends State<UserProfile> {
  Map resp = {};

  // Create a text controller and use it to retrieve the current value
  // of the TextField.
  final personController = TextEditingController();

  @override
  void dispose() {
    // Clean up the controller when the widget is disposed.
    personController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    // 編輯後更新畫面
    // resp = resp.isNotEmpty ? resp : ModalRoute.of(context)!.settings.arguments as Map<String, dynamic>;
    resp = ModalRoute.of(context)!.settings.arguments as Map<String, dynamic>;
    personController.text = resp["personCName"];

    return Scaffold(
      backgroundColor: Colors.grey[900],
      // prevent bottom overflowed while keyboard appears
      resizeToAvoidBottomInset: false,
      appBar: AppBar(
        title: Text('Person ID Card'),
        centerTitle: true,
        backgroundColor: Colors.grey[850],
        elevation: 0.0,
      ),
      body: Padding(
        padding: const EdgeInsets.fromLTRB(30.0, 40.0, 30.0, 0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            // Center(
            //   child: CircleAvatar(
            //     radius: 40.0,
            //     // backgroundImage: AssetImage('assets/thumb.jpg'),
            //   ),
            // ),
            // Divider(
            //   color: Colors.grey[700],
            //   height: 60.0,
            // ),
            Text(
              'NAME',
              style: TextStyle(
                color: Colors.grey,
                letterSpacing: 2.0,
              ),
            ),
            SizedBox(height: 10.0),
            Text(
              resp["personCName"],
              style: TextStyle(
                color: Colors.amberAccent[200],
                fontWeight: FontWeight.bold,
                fontSize: 28.0,
                letterSpacing: 2.0,
              ),
            ),
            SizedBox(height: 30.0),
            Text(
              'PERSON ID',
              style: TextStyle(
                color: Colors.grey,
                letterSpacing: 2.0,
              ),
            ),
            SizedBox(height: 10.0),
            Text(
              resp["pId"],
              style: TextStyle(
                color: Colors.amberAccent[200],
                fontWeight: FontWeight.bold,
                fontSize: 20.0,
                letterSpacing: 2.0,
              ),
            ),
            SizedBox(height: 30.0),
            Text(
              'STATUS',
              style: TextStyle(
                color: Colors.grey,
                letterSpacing: 2.0,
              ),
            ),
            SizedBox(height: 10.0),
            Text(
              resp["statusName"],
              style: TextStyle(
                color: Colors.amberAccent[200],
                fontWeight: FontWeight.bold,
                fontSize: 20.0,
                letterSpacing: 2.0,
              ),
            ),
            SizedBox(height: 30.0),
            Text(
              'SCORE',
              style: TextStyle(
                color: Colors.grey,
                letterSpacing: 2.0,
              ),
            ),
            SizedBox(height: 10.0),
            Text(
              resp["score"].toString(),
              style: TextStyle(
                color: Colors.amberAccent[200],
                fontWeight: FontWeight.bold,
                fontSize: 20.0,
                letterSpacing: 2.0,
              ),
            ),
            SizedBox(height: 30.0),
            Row(
              children: <Widget>[
                Icon(
                  Icons.email,
                  color: Colors.grey[400],
                ),
                SizedBox(width: 10.0),
                Text(
                  "${resp["pId"].trim()}@zhtech.com.tw",
                  style: TextStyle(
                    color: Colors.grey[400],
                    fontSize: 18.0,
                    letterSpacing: 1.0,
                  ),
                )
              ],
            ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        // When the user presses the button, show an alert dialog containing
        // the text that the user has entered into the text field.
        onPressed: () {
          showDialog(
            context: context,
            builder: (context) {
              return AlertDialog(
                title: const Text('Edit Name'),
                // Retrieve the text that the user has entered by using the
                // TextEditingController.
                content: SingleChildScrollView(
                  child: ListBody(
                    children: [
                      TextField(
                        controller: personController,
                      ),
                    ],
                  ),
                ),
                actions: [
                  TextButton(
                    child: const Text('Submit'),
                    onPressed: () async {
                      var erpexamPerson = {
                        "outId": 2022051001,
                        "pId": resp["pId"],
                        "personCName": personController.text,
                      };
                      await ExamPersonService.updateErpexamPerson(erpexamPerson);
                      var res = await ExamPersonService.getAppErpexamPersons("2022051001");
                      //刪除「先前所有」rout，push到新rout
                      // Navigator.pushNamedAndRemoveUntil(context,'/user_list',
                      //         (Route<dynamic> route) => false, arguments:res);

                      Navigator.pop(context, erpexamPerson);
                      Navigator.pop(context, res);
                    },
                  ),
                ],
                // content: Text(myController.text),
              );
            },
          );
        },
        tooltip: 'Show me the value!',
        child: const Icon(Icons.edit),
      ),
    );
  }
}
