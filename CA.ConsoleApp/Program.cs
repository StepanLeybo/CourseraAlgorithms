﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using CA.Algorithms.Data.Clustering;
using CA.Algorithms.Data.KargerMinCut;
using CA.Algorithms.Data.KargerMinCut.Domain;
using CA.Algorithms.Data.Knapsack;
using CA.Algorithms.Data.MedianMaintenance;
using CA.Algorithms.Data.MergeSort;
using CA.Algorithms.Data.PrimMst;
using CA.Algorithms.Data.QuickSort;
using CA.Algorithms.Data.SccKosaraju;
using CA.Algorithms.Data.SchedulingProblem;
using CA.Algorithms.Data.ShortestPathDijkstra;
using CA.Algorithms.Data.TSP;
using CA.Algorithms.Data._2Sum;
using CA.Algorithms.Implementations.Clustering;
using CA.Algorithms.Implementations.KargerMinKut;
using CA.Algorithms.Implementations.Knapsack;
using CA.Algorithms.Implementations.MedianMaintenance;
using CA.Algorithms.Implementations.MergeSort;
using CA.Algorithms.Implementations.PrimMst;
using CA.Algorithms.Implementations.QuickSort;
using CA.Algorithms.Implementations.SccKosaraju;
using CA.Algorithms.Implementations.SchedulingProblem;
using CA.Algorithms.Implementations.ShortestPathDijkstra;
using CA.Algorithms.Implementations.TSP;
using CA.Algorithms.Implementations._2Sum;
using CA.DI;
using Ninject;
using Ninject.Parameters;
using SpeedMeasure;

namespace CA.ConsoleApp
{
    class Program
    {
        static readonly IKernel Ninj = new DependencyInjection().NinjectKernel;
        static void Main()
        {
            //QuickSort();
            //MergeSort();
            //KargerMinCut();
            //DijkstraShortestPath();
            //StronglyConnectedComponent();
            //SumAlgorithm();
            //MedianMaintenance();
            //SchedulingProblem();
            //PrimMst();
            //Clustering1();
            //ClusteringBig();
            //Knapsack();
            //KnapsackBig();
            Tsp();
        }

        public static void QuickSort()
        {
            var dataManager = Ninj.Get<IGetQuickSortData>();
            var pivot = Ninj.Get<IPivot>();
            var quickSort = Ninj.Get<QuickSort>();

            var data = dataManager.GetDataArray();
            long exchCount = 0;

            quickSort.QuickSortAlgorithm(data, 0, data.Length - 1, ref exchCount);

            QuickSortPrintData(data, exchCount);
        }

        private static void QuickSortPrintData(int[] data, long exchCount)
        {
            foreach (var i in data)
            {
                Console.Write("{0} ", i);
            }

            Console.WriteLine();
            Console.WriteLine("Exchange counter: {0}", exchCount);
        }

        private static void MergeSort()
        {
            var mergeSort = Ninj.Get<MergeSort>();
            var dataManager = Ninj.Get<IGetMergeSortData>();
            Int64 inversions = 0;

            mergeSort.MergeSortCount(dataManager.GetDataArray(), ref inversions);

            Console.WriteLine("Inversions count: {0}", inversions);
        }

        private static void KargerMinCut()
        {
            var kargerData = Ninj.Get<IGetGraphKarger>();
            var kargerMinCut = Ninj.Get<IKargerMinCut>();

            //PrintGraph(kargerData.GetGraph());

            int min = 1000;

            for (int c = 0; c < 10000; c++)
            {
                int test = kargerMinCut.MinCut(kargerData.GetGraph());
                if (min > test)
                {
                    min = test;
                }

                //Console.Write("{0} ", test);
            }

            Console.WriteLine(min);
        }

        private static void PrintGraph(GraphKmc graph)
        {
            foreach (var vertex in graph.Vertices)
            {
                Console.Write("{0} ", vertex);
                foreach (var edge in graph.Edges)
                {
                    if (edge.StartPoint == vertex)
                        Console.Write("{0} ", edge.EndPoint);
                }

                Console.WriteLine();
            }
        }

        private static void DijkstraShortestPath()
        {
            var dataManager = Ninj.Get<IGetListVertexes>();
            var dijkstraShortPath = Ninj.Get<Dijkstra>();

            var path = dijkstraShortPath.Agorithm(dataManager.GetVerteces(), 1);
            var verArray = new[] {7, 37, 59, 82, 99, 115, 133, 165, 188, 197};
            
            for (var i = 0; i < path.Length; i++)
            {
                if(verArray.Contains(i))
                    Console.WriteLine("{0}:{1}", i, path[i]);
            }
        }

        private static void StronglyConnectedComponent()
        {
            var dataManager = Ninj.Get<IGetGraphScc>();
            var sccAlgorithm = Ninj.Get<SccAlgorithm>();
            const int stackSize = 1000000000;

            var thread = new Thread(() => sccAlgorithm.FindComponents(dataManager.GetGraph()), stackSize);

            thread.Start();
        }

        private static void SumAlgorithm()
        {
            var dataManager = Ninj.Get<IGet2SumData>();
            var sumAlgorithm = Ninj.Get<I2SumAlgorithm>();
            var measure = new Measure("2-Sum");

            int counter = 0;
            measure.StartMeasure();
            for (int t = 2500; t <= 4000; t++)
            {
                if (sumAlgorithm.Match2Sum(dataManager.GetData(), t))
                {
                    counter++;
                }
            }
            measure.StopMeasureDisplay();

            Console.WriteLine(counter);
        }

        private static void MedianMaintenance()
        {
            var dataManager = Ninj.Get<IGetMedianMaintenanceData>();
            var medianMaintenanceAlgorithm = Ninj.Get<MedianMaintenanceAlgorith>();

            var medianSum = 0;
            foreach (var item in dataManager.GetData())
            {
                medianSum += medianMaintenanceAlgorithm.GetMedianWithNewItem(item);
            }

            Console.WriteLine(medianSum % 10000);
        }

        private static void SchedulingProblem()
        {
            var dataManaer = Ninj.Get<IJobsDataManager>();
            var schedulingAlgorithm = new SchedulingSubstraction();
            //var schedulingAlgorithm = new SchedulingDivision();
            Console.WriteLine(schedulingAlgorithm.Execute(dataManaer.GetJobs()));
            
        }

        private static void PrimMst()
        {
            var dataManager = Ninj.Get<IGetPrimMstData>();

            var primAlgorithm = new PrimAlgorithm();

            var vertexPrims = dataManager.GetData();
            Console.WriteLine(primAlgorithm.PathLength(vertexPrims));
        }

        private static void Clustering1()
        {
            var dataManager = Ninj.Get<IClusteringData>();

            var clusteringAlgo = new ClussteringAlgorithm();

            //106
            clusteringAlgo.GetSpacing(dataManager.GetGraph(), 4);
        }

        private static void ClusteringBig()
        {
            var dataManager = Ninj.Get<IClusteringBigData>();

            var clusteringAlgo = new ClussteringAlgorithm();

            var clusteringItems = dataManager.GetData();
            //6118
            Console.WriteLine(clusteringAlgo.GetClustersNumber(clusteringItems));
        }

        private static void Knapsack()
        {
            var dataManager = Ninj.Get<IKnapsackDataManager>();

            var knapsackAlgo = new KnapsackAlgorithm();

            var data = dataManager.GetData();
            //2493893
            Console.WriteLine(knapsackAlgo.SolutionValue(data));
        }

        private static void KnapsackBig()
        {
            var dataManager = Ninj.Get<IKnapsackDataManager>();

            var knapsackAlgo = new KnapsackBigAlgorithm();

            var data = dataManager.GetData();

            var measure = new Measure("Knapsack Total");
            measure.StartMeasure();
            //4243395
            knapsackAlgo.SolutionValue(data);
            measure.StopMeasureDisplay();
        }

        private static void Tsp()
        {
            var dataManager = Ninj.Get<IGetListVerticesTsp>();
            new TspAlgorithm().ShortestPathLength(dataManager.GetList());

            //var tspAlgorithm = new TspAlgorithm();

            // const int stackSize = 1377721600;

            //tspAlgorithm.ShortestPathLength(dataManager.GetList());

            //var tspBigData = new TspBigDataAlgorithm();

            // tspBigData.ShortestPathLength(dataManager.GetList());

            //var thread = new Thread(() => tspAlgorithm.ShortestPathLength(dataManager.GetList()), stackSize);

            //thread.Start();
            // tspAlgorithm.ShortestPathLength(dataManager.GetList());
        }
    }
}
