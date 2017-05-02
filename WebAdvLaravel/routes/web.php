<?php

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
    return view('welcome');
});

//zo begint alles met api/
Route::group(['prefix' => 'api'], function(){
    //to get all products
    //Route::get('products',['as' => 'products', function(){
    //    return App\Product::all();
    //}]);

    Route::resource('products', 'ProductController', ['only' => ['index', 'store', 'update']]);
    Route::resource('products.descriptions', 'ProductDescriptionController', ['only' => ['index', 'store']]);
});


