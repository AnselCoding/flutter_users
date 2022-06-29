import 'package:flutter/material.dart';
import 'package:flutter_users/pages/loading.dart';
import 'package:flutter_users/pages/user_list.dart';
import 'package:flutter_users/pages/user_profile.dart';

void main() => runApp(MaterialApp(
    initialRoute: '/',
    routes: {
      '/': (context) => Loading(),
      '/user_list': (context) => UserList(),
      '/user_profile': (context) => UserProfile()
    }
  ));


