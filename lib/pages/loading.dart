import 'package:flutter/material.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:flutter_users/services.dart';

class Loading extends StatefulWidget {
  const Loading({Key? key}) : super(key: key);
  @override
  State<Loading> createState() => _LoadingState();
}

class _LoadingState extends State<Loading> {

  // loadingSpin() {
  //   Future.delayed(const Duration(seconds: 3), () {
  //     Navigator.pushReplacementNamed(context, "/menu");
  //   });
  // }

  getUserList() async {
    var res = await ExamPersonService.getAppErpexamPersons("2022051001");
    // 刪除「前1個」 route，push到新 route
    Navigator.pushReplacementNamed(context, "/user_list", arguments: res);
  }

  @override
   void initState() {
     super.initState();
     // loadingSpin();
     getUserList();
   }

  @override
  Widget build(BuildContext context) {

    return Scaffold(
      backgroundColor: Colors.blue[900],
      body: Center(
        child: SpinKitChasingDots(
          color: Colors.white,
          size: 70.0,
        ),
      ),
    );
  }
}
