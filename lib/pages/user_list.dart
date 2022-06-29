import 'package:flutter/material.dart';
import 'package:flutter_users/services.dart';

class UserList extends StatefulWidget {
  const UserList({Key? key}) : super(key: key);

  @override
  State<UserList> createState() => _UserListState();
}

class _UserListState extends State<UserList> {
  Map resp = {};

  // Create a text controller and use it to retrieve the current value
  // of the TextField.
  final nameController = TextEditingController();
  final pIdController = TextEditingController();

  @override
  void dispose() {
    // Clean up the controller when the widget is disposed.
    nameController.dispose();
    pIdController.dispose();
    super.dispose();
  }

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    resp = resp.isNotEmpty ? resp : ModalRoute.of(context)!.settings.arguments as Map<String, dynamic>;
    // 新增或刪除後，更新畫面
    var data = resp["data"];

    return Scaffold(
      backgroundColor: Colors.grey[200],
      appBar: AppBar(
        backgroundColor: Colors.blue[900],
        title: Text("場次2022051001 考生資料"),
        centerTitle: true,
        elevation: 0,
      ),
      body: ListView.builder(
          itemCount: data.length,
          itemBuilder: (contex, index) {
            return Padding(
              padding:
                  const EdgeInsets.symmetric(vertical: 1.0, horizontal: 4.0),
              child: Card(
                child: ListTile(
                  onTap: () async {
                    dynamic result = await Navigator.pushNamed(context, "/user_profile",
                        arguments: data[index]);
                    if(result != null){
                      setState(() {
                        resp = result;
                      });
                    }
                  },
                  leading: Icon(Icons.album),
                  title: Text(data[index]["pId"]),
                  subtitle: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(data[index]["personCName"]),
                      IconButton(
                          icon: Icon(Icons.delete),
                          onPressed: () {
                            showDialog(
                              context: context,
                              builder: (context) {
                                return AlertDialog(
                                  title: const Text('Remove User'),
                                  // Retrieve the text that the user has entered by using the
                                  // TextEditingController.
                                  content: SingleChildScrollView(
                                    child: Text(
                                      '是否確認移除此用戶${data[index]["pId"].trim()}?',
                                    ),
                                  ),
                                  actions: [
                                    TextButton(
                                      child: const Text('確認'),
                                      onPressed: () async {
                                        var erpexamPerson = {
                                          "outId": 2022051001,
                                          "pId": data[index]["pId"],
                                          "personCName": "",
                                        };
                                        await ExamPersonService.removeErpexamPerson(erpexamPerson);
                                        var res = await ExamPersonService.getAppErpexamPersons("2022051001");
                                        //刪除「先前所有」rout，push到新rout
                                        // Navigator.pushNamedAndRemoveUntil(context,'/user_list',
                                        //         (Route<dynamic> route) => false, arguments:res);
                                        setState((){
                                          resp = res;
                                        });
                                        Navigator.of(context).pop();
                                      },
                                    ),TextButton(
                                      child: const Text('取消'),
                                      onPressed: () {
                                        Navigator.of(context).pop();
                                      },
                                    ),
                                  ],
                                );
                              },
                            );
                          },
                      )
                    ],
                  ),
                  // leading: const CircleAvatar(
                  //   radius: 7,
                  // ),
                ),
              ),
            );
          }),
      floatingActionButton: FloatingActionButton(
        // When the user presses the button, show an alert dialog containing
        // the text that the user has entered into the text field.
        onPressed: () {
          showDialog(
            context: context,
            builder: (context) {
              return AlertDialog(
                title: const Text('New User'),
                // Retrieve the text that the user has entered by using the
                // TextEditingController.
                content: SingleChildScrollView(
                  child: ListBody(
                    children: [
                      const Text(
                        'NAME',
                      ),
                      TextField(
                        controller: nameController,
                      ),
                      SizedBox(height: 25.0),
                      const Text(
                        'PERSON ID',
                      ),
                      TextField(
                        controller: pIdController,
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
                        "pId": pIdController.text,
                        "personCName": nameController.text,
                      };
                      await ExamPersonService.addErpexamPerson(erpexamPerson);
                      var res = await ExamPersonService.getAppErpexamPersons("2022051001");
                      if(res != null){
                        setState(() {
                          resp = res;
                        });
                      Navigator.of(context).pop();
                      }
                    },
                  ),
                ],
              );
            },
          );
        },
        tooltip: 'Show me the value!',
        child: const Icon(Icons.add),
      ),
    );
  }
}
