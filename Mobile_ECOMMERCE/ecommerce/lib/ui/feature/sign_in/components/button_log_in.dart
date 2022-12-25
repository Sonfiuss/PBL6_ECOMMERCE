import 'package:flutter/material.dart';
// ignore: depend_on_referenced_packages
import 'package:provider/provider.dart';

import '../../../resources/app_colors.dart';
import '../view_model/sign_in_view_model.dart';

class ButtonLogin extends StatelessWidget {
  const ButtonLogin({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    double size = MediaQuery.of(context).size.width;
    return Consumer<SignInViewModel>(
      builder: (context, value, child) => SizedBox(
        width: size,
        height: 200,
        child: Stack(
          children: <Widget>[
            AnimatedPositioned(
              width: 200.0,
              height: 50.0,
              // ignore: dead_code
              top: value.signInState.check ? 150 : 50,
              // ignore: dead_code
              left: size / 2 - 120,
              duration: const Duration(seconds: 2),
              curve: Curves.fastOutSlowIn,
              child: GestureDetector(
                onTap: () {
                  value.check();
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
