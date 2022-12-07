import 'package:freezed_annotation/freezed_annotation.dart';

part 'sign_in_state.freezed.dart';

@freezed
class SignInState with _$SignInState {
  factory SignInState({
    required bool check,
   
  }) = _SignInState;

  const SignInState._();

  factory SignInState.initial() => SignInState(
        check:false,
       
      );
}
