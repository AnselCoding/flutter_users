import 'package:http/http.dart';
import 'dart:convert';

var host = "http://10.0.2.2:8059";
// get api
getHttp(String uri) async {
  var url = Uri.parse(host + uri);
  Response response = await get(url);

  Map data = jsonDecode(response.body);
  return data;
}

// post api
postHttp(String uri, dynamic value) async {
  var url = Uri.parse(host + uri);
  var body = json.encode(value);
  Response response = await post(
      url,
      headers: {"Content-Type": "application/json"},
      body: body
  );

  Map data = jsonDecode(response.body);
  return data;
}

// api service
class ExamPersonService{
  static getAppErpexamPersons(outId){
    return getHttp("/api/ExamPerson/GetAppErpexamPersons?outId=$outId");
  }
  static updateErpexamPerson(data){
    return postHttp("/api/ExamPerson/UpdateErpexamPerson",data);
  }
  static addErpexamPerson(data){
    return postHttp("/api/ExamPerson/AddErpexamPerson",data);
  }
  static removeErpexamPerson(data){
    return postHttp("/api/ExamPerson/RemoveErpexamPerson",data);
  }

  // static getQuesPersonRecord(outId,pId){
  //   return getHttp("/api/ExamPerson/GetQuesPersonRecord?outId=$outId&pId=$pId");
  // }
  //改成編輯考生資料
  // static setPersonAppScore(data){
  //   return postHttp("/api/ExamPerson/SetPersonAppScore",data);
  // }
}
