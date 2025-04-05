module.exports = {
    root: true,
    parser: '@typescript-eslint/parser',
    plugins: ['react', 'tailwindcss', '@typescript-eslint', 'react-hooks'],
    extends: [
      'eslint:recommended',
      'plugin:react/recommended',
      'plugin:tailwindcss/recommended',
      'plugin:@typescript-eslint/recommended',
      'prettier'
    ],
    rules: {
      'react/react-in-jsx-scope': 'off',
      '@typescript-eslint/no-unused-vars': ['warn'],
      'tailwindcss/no-custom-classname': 'off'
    },
    settings: {
      react: {
        version: 'detect'
      }
    }
  }
  