# Field-Map Auto Create 0.1.2v - 2025-11-25
### 이 브랜치는 자동 조립식 맵을 개발하는 브랜치 입니다.

## 기능 설명
Lost-Bullet 프로젝트에서는 조립식 맵 구조를 사용 하기 때문에 이러한 기능을 개발했습니다.  
AutoMap.cs 스크립트 파일을 Empty/GameObject에 컴포넌트로 넣게 된다면 AutoMap.cs에 맵 파일   
오브젝트를 넣아야 할 공간에 넣으면 맵파일이 자동으로 구조체에 들어갑니다.  
만약 소스 코드를 통해  MapControl.cs 스크립트 파일이 없다면 AutoMap.cs 스크립트의 폴더 주소 값을  
가져와 같은 공간에 MapControl.cs 파일을 제작 후 MapControl에서 AutoMap.cs에 있는  
 필드맵 파일을 사용 할 수 있게 공유한다.

### AutoMap.cs 스크립트 파일의 구성
- 만약 MapControl.cs가 없다면 생성하기
- 맵 GameObject.Transform을 넣는 공간  
- 부모 맵에 있는 자식 필드맵을 구조체에 대입하기
    - 구조체 구성
        - 구조체 이름 (string)
        - 자식 오브젝트 (GameObject[])

## 기능 추가 내용
### [AutoMap.cs](https://github.com/BestBlackCube/Lost-Bullet-Github/commit/cb4c7d3d7a818fdf34df71ec69785e1d1370673b#diff-16086e27b3578a17f5e0af794353e61ca9d8f788380da5e9d7dbd0a9f5905802)
- 지상 필드맵과 하늘 필드맵을 구별하여 Count로 나누어 계산한다.
    - FieldAutoSetup()

### [MapContorl.cs](https://github.com/BestBlackCube/Lost-Bullet-Github/commit/cb4c7d3d7a818fdf34df71ec69785e1d1370673b#diff-aebf9fbe8a371b62b47ab144458ecf3ef45cad66761c0d961a938355ae691585)
- Field 오브젝트에 들어있는 여러가 필드맵을 난수로 정해진 필드 코드값을 불러와 생성한다.
    - CreateAllMap()

## [개발 단계]
### 개발 된 기능
- [x] 필드맵 구조를 자동 인식하는 구조체
- [x] 필드맵 제작을 하는 컨트롤러

### 개발 중인 기능
- [ ] 필드맵 지하맵
- [ ] 필드맵 지상맵
- [ ] 필드맵 하늘맵

### 수정 해야할 기능
- [x]


## 버전 표기법 (Semantic Versioning)
```
[주 버전].[부 버전].[수 버전]
   0   .   0   .   0

- 주 버전: 하위 호환성이 깨지는 변경
- 부 버전: 하위 호환성 유지하며 기능 추가
- 수 버전: 하위 호환성 유지하며 버그 수정
