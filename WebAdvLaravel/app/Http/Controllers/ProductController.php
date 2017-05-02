<?php

namespace App\Http\Controllers;


use Illuminate\Http\Request;
use Illuminate\Http\Response;
use App\Product;

class ProductController extends Controller
{
    /**
     * @return Response
     */
    public function index(){
        return Product::paginate();
    }

    /**
     * @param Request $request
     * @return Response
     */
    public function store(Request $request){
        return Product::create([
            'name' => $request->input('name')
        ]);
    }

    /**
     * @param Request $request
     * @param int $id
     * @return Response
     */
    public function update(Request $request, $id){
        $product = Product::findOrFail($id);

        $product->update([
            'name' => $request->input('name')
        ]);
    }
}
