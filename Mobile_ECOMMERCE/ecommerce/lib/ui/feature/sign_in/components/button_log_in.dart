import 'package:ecommerce/ui/feature/sign_in/bloc/sign_in_presenter.dart';
import 'package:ecommerce/ui/feature/sign_in/bloc/sign_in_state.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
// ignore: depend_on_referenced_packages
import 'package:provider/provider.dart';

import '../../../resources/app_colors.dart';
import '../../detail/bloc/detail_presenter.dart';


class ButtonLogin extends StatelessWidget {
  const ButtonLogin({Key? key, required this.signInPresenter}) : super(key: key);

  final SignInPresenter signInPresenter;

  @override
  Widget build(BuildContext context) {
    double size = MediaQuery.of(context).size.width;
    return 
      BlocBuilder<SignInPresenter, SignInState>(
        bloc: signInPresenter,
        builder: (context, state) => 
       SizedBox(
          width: size,
          height: 200,
          child: Stack(
            children: <Widget>[
              AnimatedPositioned(
                width: 200.0,
                height: 50.0,
                // ignore: dead_code
                top: state.check ? 150 : 50,
                // ignore: dead_code
                left: size / 2 - 120,
                duration: const Duration(seconds: 2),
                curve: Curves.fastOutSlowIn,
                child: GestureDetector(
                  onTap: () {
                    (state.email.isEmpty && state.password.isEmpty)?
                    signInPresenter.check():signInPresenter.onTapSignIn();
                  },
                  child: Container(
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(15),
                      color: const Color.fromARGB(255, 14, 91, 154),
                    ),
                    child: const Center(
                      child: Text(
                        'Login',
                        style: TextStyle(
                          color: AppColors.white,
                          fontWeight: FontWeight.w700,
                          fontSize: 18,
                        ),
                      ),
                    ),
                  ),
                ),
              ),
            ],
          ),
           
          ),
      );
  }
}
