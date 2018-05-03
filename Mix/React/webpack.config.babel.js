import path from 'path';

export default {
    mode: "production",
    entry: [
        './src/index.js'
    ],
    output: {
        filename: 'mix.js',
        path: path.resolve(__dirname, 'dist')
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                include: path.join(__dirname, 'src'),
                loader: 'babel-loader'
            },
        ]
    }
};