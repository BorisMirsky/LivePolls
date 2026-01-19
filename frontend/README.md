         


Поставить next:
\frontend>npx create-next-app@latest



поставить antd:
переход в папку проекта
npm install antd --save
ошибка: см скриншот


#######################################################################



ДОБАВИть (?)
.tsconfig.json --> compilerOptions
"compilerOptions": {
    "paths": {
      "react": [ "./node_modules/@types/react" ]
    }
 }


                Добавить как патч (React19 compatibility):
   1) npm install @ant-design/v5-patch-for-react-19 --save
   2) Import the compatibility package at the application entry:
          import '@ant-design/v5-patch-for-react-19';  

















