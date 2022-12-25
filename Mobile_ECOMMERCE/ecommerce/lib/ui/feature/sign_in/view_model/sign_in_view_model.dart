import 'package:ecommerce/ui/feature/sign_in/view_model/sign_in_state.dart';
import 'package:flutter/material.dart';

class SignInViewModel with ChangeNotifier {
  SignInState signInState = SignInState.initial();
  void check() {
    signInState = signInState.copyWith(check: !signInState.check);
    notifyListeners();
  }
}
