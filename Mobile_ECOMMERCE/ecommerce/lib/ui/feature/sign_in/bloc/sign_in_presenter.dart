import 'package:dio/dio.dart';
import 'package:ecommerce/data/model/cart.dart';
import 'package:ecommerce/data/model/order/item_order_dtos.dart';
import 'package:ecommerce/data/model/order/order.dart';
import 'package:ecommerce/data/model/sign_in/user.dart';
import 'package:ecommerce/data/model/source%20data/api_client.dart';
import 'package:ecommerce/ui/feature/cart/bloc/cart_state.dart';
import 'package:ecommerce/ui/feature/pay/components/list_order.dart';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'sign_in_state.dart';

class SignInPresenter extends Cubit<SignInState> {
  SignInPresenter({
    @visibleForTesting SignInState? state,
  }) : super(
          state ?? SignInState.initial(),
        );
  TextEditingController textEditingControllerEmail = TextEditingController();
  TextEditingController textEditingControllerPassword = TextEditingController();
  ApiClient apiClient = ApiClient(Dio());
  void check() {
    emit(state.copyWith(check: !state.check));
  }

  void inputEmail(String email) {
    emit(state.copyWith(email: email));
  }

  void inputPassword(String password) {
    emit(state.copyWith(password: password));
  }

  void onTapSignIn() async {
    User user = User(username: state.email, password: state.password);
    final authlogin = await apiClient.postLognIn(user);
    emit(state.copyWith(token: authlogin.result.token));
  }
}
