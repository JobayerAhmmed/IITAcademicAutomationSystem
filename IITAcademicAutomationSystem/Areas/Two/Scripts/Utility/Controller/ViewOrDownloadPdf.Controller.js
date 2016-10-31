(
    function () {
    	angular.module("utility_module", ['ngPDFViewer']).controller('viewOrDownloadPdf_controller', ["$scope", 'PDFViewerService', function ($scope, pdf) {
    		$scope.name = "Ishmam";
    		console.log('TestController: new instance');

    		$scope.pdfURL = "test.pdf";

    		$scope.instance = pdf.Instance("viewer");

    		$scope.nextPage = function () {
    			$scope.instance.nextPage();
    		};

    		$scope.prevPage = function () {
    			$scope.instance.prevPage();
    		};

    		$scope.gotoPage = function (page) {
    			$scope.instance.gotoPage(page);
    		};

    		$scope.pageLoaded = function (curPage, totalPages) {
    			$scope.currentPage = curPage;
    			$scope.totalPages = totalPages;
    		};

    		$scope.loadProgress = function (loaded, total, state) {
    			console.log('loaded =', loaded, 'total =', total, 'state =', state);
    		};
    		
    	}])
    }()
)



