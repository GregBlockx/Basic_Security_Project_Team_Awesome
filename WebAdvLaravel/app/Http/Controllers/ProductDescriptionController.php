<?php

namespace App\Http\Controllers;

use App\Description;
use App\Product;
use Illuminate\Http\Request;

class ProductDescriptionController extends Controller
{
    /**
     * @return Response
     */
    public function index($productId){
        return Description::ofProduct($productId)->paginate();
    }

    /**
     * @param Request $request
     * @return Response
     */
    public function store($productId, Request $request){
        $product = Product::findOrFail($productId);

        $product->descriptions()->save(new Description([
            'body' => $request->input('body')
        ]));
        return $product->descriptions;
    }
}
