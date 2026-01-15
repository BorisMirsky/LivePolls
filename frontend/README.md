         


Поставил next:
\frontend>npx create-next-app@latest



Попытка поставить antd:
переход в папку проекта
npm install antd --save
ошибка: см скриншот


#######################################################################

Потом:
Чтобы завелся запуск npm run dev надо в PSh:
Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy Unrestricted


ДОБАВИЛ, НО ПОПРОБОВАТЬ УБРАТЬ, ВОЗМОЖНО ЭТО БОЛЬШЕ НЕ НУЖНО
.tsconfig.json --> compilerOptions
"compilerOptions": {
    "paths": {
      "react": [ "./node_modules/@types/react" ]
    }
 }


                Добавил как патч (React19 compatibility):
   1) npm install @ant-design/v5-patch-for-react-19 --save
   2) Import the compatibility package at the application entry:
          import '@ant-design/v5-patch-for-react-19';  



global.css - всё стёр, но не показал что туда вставляет!
а у него там страница заоплнена (1.32.25)













