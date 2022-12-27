import 'package:ecommerce/data/model/home/data.dart';
import 'package:freezed_annotation/freezed_annotation.dart';
part 'home_state.freezed.dart';

enum DetailStatus { init, inProgress, success }

@Freezed(makeCollectionsUnmodifiable: false)
class HomeState with _$HomeState {
  factory HomeState({
    required List<Data> productHome,
  }) = _HomeState;

  const HomeState._();

  factory HomeState.initial() => HomeState(
        productHome: [],
      );
}
