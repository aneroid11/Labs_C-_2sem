#include <stdio.h>
#include <stdlib.h>

#define SWAP( type, a, b ) {\
	type t = a;\
	a = b;\
	b = t;\
}

typedef unsigned char byte_t;

typedef struct {
	byte_t r, g, b;
} color_t;

typedef struct {
	int width, height;
	color_t* pixels;
} ppmImage_t;

__attribute__((fastcall)) color_t RGB( byte_t r, byte_t g, byte_t b ) {
	color_t c = { r, g, b };
	return c;
}

void PPMImage_Create( ppmImage_t* this, int w, int h ) {
	this->width = w;
	this->height = h;
	this->pixels = malloc( sizeof( color_t[ w * h ] ) );
	
	for ( int i = 0; i < w * h; i++ ) {
		this->pixels[ i ] = RGB( 0, 0, 0 );
	}
}

void PPMImage_LoadFromFile( ppmImage_t* this, const char* file ) {
	FILE* fin = fopen( file, "rb" );
	char type[ 5 ];
	fscanf( fin, "%s", type );
	
	if ( type[ 0 ] != 'P' || type[ 1 ] != '6' ) {
		fclose( fin );
		fprintf( stderr, "Image format is not P6!\n" );
		return;
	}
	
	int trash;
	fscanf( fin, "%d %d %d", &this->width, &this->height, &trash );
	fgetc( fin );
	this->pixels = malloc( sizeof( color_t[ this->width * this->height ] ) );
	
	for ( int i = 0; i < this->width * this->height; i++ ) {
		byte_t r, g, b;
		r = fgetc( fin ); g = fgetc( fin ); b = fgetc( fin );
		this->pixels[ i ] = RGB( r, g, b );
	}
	
	fclose( fin );
}

void PPMImage_WriteInFile( ppmImage_t* this, const char* file ) {
	FILE* fout = fopen( file, "wb" );
	fprintf( fout, "P6 %d %d 255 ", this->width, this->height );
	
	for ( int i = 0; i < this->width * this->height; i++ ) {
		fprintf( fout, "%c%c%c", this->pixels[ i ].r, this->pixels[ i ].g, this->pixels[ i ].b );
	}
	
	fclose( fout );
}

void PPMImage_Destroy( ppmImage_t* this ) {
	free( this->pixels );
}

void PPMImage_Clear( ppmImage_t* this, color_t cl ) {
	for ( int i = 0; i < this->width * this->height; i++ ) {
		this->pixels[ i ] = cl;
	}
}

__attribute__((fastcall)) void PPMImage_PutPixel( ppmImage_t* this, int x, int y, color_t cl ) {
	if ( x >= 0 && y >= 0 && x < this->width && y < this->height ) {
		this->pixels[ y * this->width + x ] = cl;
	}
}

__attribute__((fastcall)) color_t PPMImage_GetPixel( ppmImage_t* this, int x, int y ) {
	if ( x >= 0 && y >= 0 && x < this->width && y < this->height ) {
		return this->pixels[ y * this->width + x ];
	}
}

void PPMImage_FlipVertically( ppmImage_t* this ) {
	for ( int x = 0; x < this->width; x++ ) {
		for ( int y = 0; y < this->height / 2; y++ ) {
			SWAP( color_t, this->pixels[ y * this->width + x ], this->pixels[ ( this->height - y - 1 ) * this->width + x ] );
		}
	}
}

void PPMImage_FlipHorizontally( ppmImage_t* this ) {
	for ( int y = 0; y < this->height; y++ ) {
		for ( int x = 0; x < this->width / 2; x++ ) {
			SWAP( color_t, this->pixels[ y * this->width + x ], this->pixels[ y * this->width + ( this->width - x - 1 ) ] );
		}
	}
}
