﻿/*
* The author of this software is Steven Fortune.  Copyright (c) 1994 by AT&T
* Bell Laboratories.
* Permission to use, copy, modify, and distribute this software for any
* purpose without fee is hereby granted, provided that this entire notice
* is included in all copies of any software which is or includes a copy
* or modification of this software and in all copies of the supporting
* documentation for such software.
* THIS SOFTWARE IS BEING PROVIDED "AS IS", WITHOUT ANY EXPRESS OR IMPLIED
* WARRANTY.  IN PARTICULAR, NEITHER THE AUTHORS NOR AT&T MAKE ANY
* REPRESENTATION OR WARRANTY OF ANY KIND CONCERNING THE MERCHANTABILITY
* OF THIS SOFTWARE OR ITS FITNESS FOR ANY PARTICULAR PURPOSE.
*/

/* 
 * This code was originally written by Stephan Fortune in C code.  I, Shane O'Sullivan,
 * have since modified it, encapsulating it in a C++ class and, fixing memory leaks and
 * adding accessors to the Voronoi Edges.
 * Permission to use, copy, modify, and distribute this software for any
 * purpose without fee is hereby granted, provided that this entire notice
 * is included in all copies of any software which is or includes a copy
 * or modification of this software and in all copies of the supporting
 * documentation for such software.
 * THIS SOFTWARE IS BEING PROVIDED "AS IS", WITHOUT ANY EXPRESS OR IMPLIED
 * WARRANTY.  IN PARTICULAR, NEITHER THE AUTHORS NOR AT&T MAKE ANY
 * REPRESENTATION OR WARRANTY OF ANY KIND CONCERNING THE MERCHANTABILITY
 * OF THIS SOFTWARE OR ITS FITNESS FOR ANY PARTICULAR PURPOSE.
 */

/* 
 * Java Version by Zhenyu Pan
 * Permission to use, copy, modify, and distribute this software for any
 * purpose without fee is hereby granted, provided that this entire notice
 * is included in all copies of any software which is or includes a copy
 * or modification of this software and in all copies of the supporting
 * documentation for such software.
 * THIS SOFTWARE IS BEING PROVIDED "AS IS", WITHOUT ANY EXPRESS OR IMPLIED
 * WARRANTY.  IN PARTICULAR, NEITHER THE AUTHORS NOR AT&T MAKE ANY
 * REPRESENTATION OR WARRANTY OF ANY KIND CONCERNING THE MERCHANTABILITY
 * OF THIS SOFTWARE OR ITS FITNESS FOR ANY PARTICULAR PURPOSE.
 */

/*
* C# Version by Burhan Joukhadar
* 
* Permission to use, copy, modify, and distribute this software for any
* purpose without fee is hereby granted, provided that this entire notice
* is included in all copies of any software which is or includes a copy
* or modification of this software and in all copies of the supporting
* documentation for such software.
* THIS SOFTWARE IS BEING PROVIDED "AS IS", WITHOUT ANY EXPRESS OR IMPLIED
* WARRANTY.  IN PARTICULAR, NEITHER THE AUTHORS NOR AT&T MAKE ANY
* REPRESENTATION OR WARRANTY OF ANY KIND CONCERNING THE MERCHANTABILITY
* OF THIS SOFTWARE OR ITS FITNESS FOR ANY PARTICULAR PURPOSE.
*/

using System;
using System.Collections.Generic;

namespace Sceelix.Paths.Algorithms
{
    /// <summary>
    /// Description of Voronoi.
    /// </summary>
    public class Voronoi
    {
        private const int Le = 0;
        private const int Re = 1;
        private readonly double _minDistanceBetweenSites;

        private List<GraphEdge> _allEdges;

        // ************* Private members ******************
        private double _borderMinX, _borderMaxX, _borderMinY, _borderMaxY;
        private Site _bottomsite;
        private Halfedge[] _eLhash;

        private int _eLhashsize;
        private Halfedge _eLleftend, _eLrightend;
        private int _nedges;
        private int _nsites;
        private int _nvertices;
        private int _pQcount;
        private Halfedge[] _pQhash;
        private int _pQhashsize;
        private int _pQmin;
        private int _siteidx;
        private Site[] _sites;
        private int _sqrtNsites;
        private double _xmin, _xmax, _ymin, _ymax, _deltax, _deltay;


        // ************* Public methods ******************
        // ******************************************



        // constructor
        public Voronoi(double minDistanceBetweenSites)
        {
            _siteidx = 0;
            _sites = null;

            _allEdges = null;
            _minDistanceBetweenSites = minDistanceBetweenSites;
        }



        private Edge Bisect(Site s1, Site s2)
        {
            double dx, dy, adx, ady;
            Edge newedge;

            newedge = new Edge();

            newedge.reg[0] = s1;
            newedge.reg[1] = s2;

            newedge.ep[0] = null;
            newedge.ep[1] = null;

            dx = s2.coord.x - s1.coord.x;
            dy = s2.coord.y - s1.coord.y;

            adx = dx > 0 ? dx : -dx;
            ady = dy > 0 ? dy : -dy;
            newedge.c = s1.coord.x * dx + s1.coord.y * dy + (dx * dx + dy * dy) * 0.5;

            if (adx > ady)
            {
                newedge.a = 1.0;
                newedge.b = dy / dx;
                newedge.c /= dx;
            }
            else
            {
                newedge.a = dx / dy;
                newedge.b = 1.0;
                newedge.c /= dy;
            }

            newedge.edgenbr = _nedges;
            _nedges++;

            return newedge;
        }



        private void clip_line(Edge e)
        {
            double pxmin, pxmax, pymin, pymax;
            Site s1, s2;

            double x1 = e.reg[0].coord.x;
            double y1 = e.reg[0].coord.y;
            double x2 = e.reg[1].coord.x;
            double y2 = e.reg[1].coord.y;
            double x = x2 - x1;
            double y = y2 - y1;

            // if the distance between the two points this line was created from is
            // less than the square root of 2 عن جد؟, then ignore it
            if (Math.Sqrt(x * x + y * y) < _minDistanceBetweenSites) return;
            pxmin = _borderMinX;
            pymin = _borderMinY;
            pxmax = _borderMaxX;
            pymax = _borderMaxY;

            if (e.a == 1.0 && e.b >= 0.0)
            {
                s1 = e.ep[1];
                s2 = e.ep[0];
            }
            else
            {
                s1 = e.ep[0];
                s2 = e.ep[1];
            }

            if (e.a == 1.0)
            {
                y1 = pymin;

                if (s1 != null && s1.coord.y > pymin)
                    y1 = s1.coord.y;
                if (y1 > pymax)
                    y1 = pymax;
                x1 = e.c - e.b * y1;
                y2 = pymax;

                if (s2 != null && s2.coord.y < pymax)
                    y2 = s2.coord.y;
                if (y2 < pymin)
                    y2 = pymin;
                x2 = e.c - e.b * y2;
                if (((x1 > pxmax) & (x2 > pxmax)) | ((x1 < pxmin) & (x2 < pxmin)))
                    return;

                if (x1 > pxmax)
                {
                    x1 = pxmax;
                    y1 = (e.c - x1) / e.b;
                }

                if (x1 < pxmin)
                {
                    x1 = pxmin;
                    y1 = (e.c - x1) / e.b;
                }

                if (x2 > pxmax)
                {
                    x2 = pxmax;
                    y2 = (e.c - x2) / e.b;
                }

                if (x2 < pxmin)
                {
                    x2 = pxmin;
                    y2 = (e.c - x2) / e.b;
                }
            }
            else
            {
                x1 = pxmin;
                if (s1 != null && s1.coord.x > pxmin)
                    x1 = s1.coord.x;
                if (x1 > pxmax)
                    x1 = pxmax;
                y1 = e.c - e.a * x1;

                x2 = pxmax;
                if (s2 != null && s2.coord.x < pxmax)
                    x2 = s2.coord.x;
                if (x2 < pxmin)
                    x2 = pxmin;
                y2 = e.c - e.a * x2;

                if (((y1 > pymax) & (y2 > pymax)) | ((y1 < pymin) & (y2 < pymin)))
                    return;

                if (y1 > pymax)
                {
                    y1 = pymax;
                    x1 = (e.c - y1) / e.a;
                }

                if (y1 < pymin)
                {
                    y1 = pymin;
                    x1 = (e.c - y1) / e.a;
                }

                if (y2 > pymax)
                {
                    y2 = pymax;
                    x2 = (e.c - y2) / e.a;
                }

                if (y2 < pymin)
                {
                    y2 = pymin;
                    x2 = (e.c - y2) / e.a;
                }
            }

            PushGraphEdge(e.reg[0], e.reg[1], x1, y1, x2, y2);
        }



        private double dist(Site s, Site t)
        {
            double dx, dy;
            dx = s.coord.x - t.coord.x;
            dy = s.coord.y - t.coord.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }



        /*
         * This delete routine can't reclaim node, since pointers from hash table
         * may be present.
         */



        private void ELdelete(Halfedge he)
        {
            he.ELleft.ELright = he.ELright;
            he.ELright.ELleft = he.ELleft;
            he.deleted = true;
        }



        /* Get entry from hash table, pruning any deleted nodes */



        private Halfedge ELgethash(int b)
        {
            Halfedge he;
            if (b < 0 || b >= _eLhashsize)
                return null;

            he = _eLhash[b];
            if (he == null || !he.deleted)
                return he;

            /* Hash table points to deleted half edge. Patch as necessary. */
            _eLhash[b] = null;
            return null;
        }



        private bool ELinitialize()
        {
            _eLhashsize = 2 * _sqrtNsites;
            _eLhash = new Halfedge[_eLhashsize];

            for (int i = 0; i < _eLhashsize; i++) _eLhash[i] = null;

            _eLleftend = HEcreate(null, 0);
            _eLrightend = HEcreate(null, 0);
            _eLleftend.ELleft = null;
            _eLleftend.ELright = _eLrightend;
            _eLrightend.ELleft = _eLleftend;
            _eLrightend.ELright = null;
            _eLhash[0] = _eLleftend;
            _eLhash[_eLhashsize - 1] = _eLrightend;

            return true;
        }



        private void ELinsert(Halfedge lb, Halfedge newHe)
        {
            newHe.ELleft = lb;
            newHe.ELright = lb.ELright;
            lb.ELright.ELleft = newHe;
            lb.ELright = newHe;
        }



        private Halfedge ELleft(Halfedge he)
        {
            return he.ELleft;
        }



        private Halfedge ELleftbnd(Point p)
        {
            int bucket;
            Halfedge he;

            /* Use hash table to get close to desired halfedge */
            // use the hash function to find the place in the hash map that this
            // HalfEdge should be
            bucket = (int) ((p.x - _xmin) / _deltax * _eLhashsize);

            // make sure that the bucket position is within the range of the hash
            // array
            if (bucket < 0) bucket = 0;
            if (bucket >= _eLhashsize) bucket = _eLhashsize - 1;

            he = ELgethash(bucket);

            // if the HE isn't found, search backwards and forwards in the hash map
            // for the first non-null entry
            if (he == null)
                for (int i = 1; i < _eLhashsize; i++)
                {
                    if ((he = ELgethash(bucket - i)) != null)
                        break;
                    if ((he = ELgethash(bucket + i)) != null)
                        break;
                }

            /* Now search linear list of halfedges for the correct one */
            if (he == _eLleftend || he != _eLrightend && right_of(he, p))
            {
                // keep going right on the list until either the end is reached, or
                // you find the 1st edge which the point isn't to the right of
                do
                {
                    he = he.ELright;
                } while (he != _eLrightend && right_of(he, p));

                he = he.ELleft;
            }
            else
                // if the point is to the left of the HalfEdge, then search left for
                // the HE just to the left of the point
            {
                do
                {
                    he = he.ELleft;
                } while (he != _eLleftend && !right_of(he, p));
            }

            /* Update hash table and reference counts */
            if (bucket > 0 && bucket < _eLhashsize - 1) _eLhash[bucket] = he;

            return he;
        }



        private Halfedge ELright(Halfedge he)
        {
            return he.ELright;
        }



        private void endpoint(Edge e, int lr, Site s)
        {
            e.ep[lr] = s;
            if (e.ep[Re - lr] == null)
                return;
            clip_line(e);
        }



        /**
         * 
         * @param xValuesIn Array of X values for each site.
         * @param yValuesIn Array of Y values for each site. Must be identical length to yValuesIn
         * @param minX The minimum X of the bounding box around the voronoi
         * @param maxX The maximum X of the bounding box around the voronoi
         * @param minY The minimum Y of the bounding box around the voronoi
         * @param maxY The maximum Y of the bounding box around the voronoi
         * @return
         */
        public List<GraphEdge> GenerateVoronoi(double[] xValuesIn, double[] yValuesIn, double minX, double maxX, double minY, double maxY)
        {
            Sort(xValuesIn, yValuesIn, xValuesIn.Length);

            // Check bounding box inputs - if mins are bigger than maxes, swap them
            double temp = 0;
            if (minX > maxX)
            {
                temp = minX;
                minX = maxX;
                maxX = temp;
            }

            if (minY > maxY)
            {
                temp = minY;
                minY = maxY;
                maxY = temp;
            }

            _borderMinX = minX;
            _borderMinY = minY;
            _borderMaxX = maxX;
            _borderMaxY = maxY;

            _siteidx = 0;
            voronoi_bd();
            return _allEdges;
        }



        private Halfedge HEcreate(Edge e, int pm)
        {
            Halfedge answer = new Halfedge();
            answer.ELedge = e;
            answer.ELpm = pm;
            answer.PQnext = null;
            answer.vertex = null;

            return answer;
        }



        // create a new site where the HalfEdges el1 and el2 intersect - note that
        // the Point in the argument list is not used, don't know why it's there
        private Site intersect(Halfedge el1, Halfedge el2)
        {
            Edge e1, e2, e;
            Halfedge el;
            double d, xint, yint;
            bool right_of_site;
            Site v; // vertex

            e1 = el1.ELedge;
            e2 = el2.ELedge;

            if (e1 == null || e2 == null)
                return null;

            // if the two edges bisect the same parent, return null
            if (e1.reg[1] == e2.reg[1])
                return null;

            d = e1.a * e2.b - e1.b * e2.a;
            if (-1.0e-10 < d && d < 1.0e-10)
                return null;

            xint = (e1.c * e2.b - e2.c * e1.b) / d;
            yint = (e2.c * e1.a - e1.c * e2.a) / d;

            if (e1.reg[1].coord.y < e2.reg[1].coord.y
                || e1.reg[1].coord.y == e2.reg[1].coord.y && e1.reg[1].coord.x < e2.reg[1].coord.x)
            {
                el = el1;
                e = e1;
            }
            else
            {
                el = el2;
                e = e2;
            }

            right_of_site = xint >= e.reg[1].coord.x;
            if (right_of_site && el.ELpm == Le
                || !right_of_site && el.ELpm == Re)
                return null;

            // create a new site at the point of intersection - this is a new vector
            // event waiting to happen
            v = new Site();
            v.coord.x = xint;
            v.coord.y = yint;
            return v;
        }



        private Site Leftreg(Halfedge he)
        {
            if (he.ELedge == null) return _bottomsite;
            return he.ELpm == Le ? he.ELedge.reg[Le] : he.ELedge.reg[Re];
        }



        private void makevertex(Site v)
        {
            v.sitenbr = _nvertices;
            _nvertices++;
        }



        private Site Nextone()
        {
            Site s;
            if (_siteidx < _nsites)
            {
                s = _sites[_siteidx];
                _siteidx++;
                return s;
            }

            return null;
        }



        private Point PQ_min()
        {
            Point answer = new Point();

            while (_pQhash[_pQmin].PQnext == null) _pQmin++;

            answer.x = _pQhash[_pQmin].PQnext.vertex.coord.x;
            answer.y = _pQhash[_pQmin].PQnext.ystar;
            return answer;
        }



        private int PQbucket(Halfedge he)
        {
            int bucket;

            bucket = (int) ((he.ystar - _ymin) / _deltay * _pQhashsize);
            if (bucket < 0)
                bucket = 0;
            if (bucket >= _pQhashsize)
                bucket = _pQhashsize - 1;
            if (bucket < _pQmin)
                _pQmin = bucket;

            return bucket;
        }



        // remove the HalfEdge from the list of vertices
        private void PQdelete(Halfedge he)
        {
            Halfedge last;

            if (he.vertex != null)
            {
                last = _pQhash[PQbucket(he)];
                while (last.PQnext != he) last = last.PQnext;

                last.PQnext = he.PQnext;
                _pQcount--;
                he.vertex = null;
            }
        }



        private bool PQempty()
        {
            return _pQcount == 0;
        }



        private Halfedge PQextractmin()
        {
            Halfedge curr;

            curr = _pQhash[_pQmin].PQnext;
            _pQhash[_pQmin].PQnext = curr.PQnext;
            _pQcount--;

            return curr;
        }



        private bool PQinitialize()
        {
            _pQcount = 0;
            _pQmin = 0;
            _pQhashsize = 4 * _sqrtNsites;
            _pQhash = new Halfedge[_pQhashsize];

            for (int i = 0; i < _pQhashsize; i++) _pQhash[i] = new Halfedge();
            return true;
        }



        // push the HalfEdge into the ordered linked list of vertices
        private void PQinsert(Halfedge he, Site v, double offset)
        {
            Halfedge last, next;

            he.vertex = v;
            he.ystar = v.coord.y + offset;
            last = _pQhash[PQbucket(he)];

            while
            (
                (next = last.PQnext) != null
                &&
                (he.ystar > next.ystar || he.ystar == next.ystar && v.coord.x > next.vertex.coord.x)
            )
                last = next;

            he.PQnext = last.PQnext;
            last.PQnext = he;
            _pQcount++;
        }



        private void PushGraphEdge(Site leftSite, Site rightSite, double x1, double y1, double x2, double y2)
        {
            GraphEdge newEdge = new GraphEdge();
            _allEdges.Add(newEdge);
            newEdge.x1 = x1;
            newEdge.y1 = y1;
            newEdge.x2 = x2;
            newEdge.y2 = y2;

            newEdge.site1 = leftSite.sitenbr;
            newEdge.site2 = rightSite.sitenbr;
        }



        private void Qsort(Site[] sites)
        {
            List<Site> listSites = new List<Site>(sites.Length);
            for (int i = 0; i < sites.Length; i++) listSites.Add(sites[i]);

            listSites.Sort(new SiteSorterYX());

            // Copy back into the array
            for (int i = 0; i < sites.Length; i++) sites[i] = listSites[i];
        }



        /* returns true if p is to right of halfedge e */



        private bool right_of(Halfedge el, Point p)
        {
            Edge e;
            Site topsite;
            bool right_of_site;
            bool above, fast;
            double dxp, dyp, dxs, t1, t2, t3, yl;

            e = el.ELedge;
            topsite = e.reg[1];

            if (p.x > topsite.coord.x)
                right_of_site = true;
            else
                right_of_site = false;

            if (right_of_site && el.ELpm == Le)
                return true;
            if (!right_of_site && el.ELpm == Re)
                return false;

            if (e.a == 1.0)
            {
                dxp = p.x - topsite.coord.x;
                dyp = p.y - topsite.coord.y;
                fast = false;

                if ((!right_of_site & (e.b < 0.0)) | (right_of_site & (e.b >= 0.0)))
                {
                    above = dyp >= e.b * dxp;
                    fast = above;
                }
                else
                {
                    above = p.x + p.y * e.b > e.c;
                    if (e.b < 0.0)
                        above = !above;
                    if (!above)
                        fast = true;
                }

                if (!fast)
                {
                    dxs = topsite.coord.x - e.reg[0].coord.x;
                    above = e.b * (dxp * dxp - dyp * dyp)
                            < dxs * dyp * (1.0 + 2.0 * dxp / dxs + e.b * e.b);

                    if (e.b < 0)
                        above = !above;
                }
            }
            else // e.b == 1.0
            {
                yl = e.c - e.a * p.x;
                t1 = p.y - yl;
                t2 = p.x - topsite.coord.x;
                t3 = yl - topsite.coord.y;
                above = t1 * t1 > t2 * t2 + t3 * t3;
            }

            return el.ELpm == Le ? above : !above;
        }



        private Site Rightreg(Halfedge he)
        {
            if (he.ELedge == null)
                // if this halfedge has no edge, return the bottom site (whatever
                // that is)
                return _bottomsite;

            // if the ELpm field is zero, return the site 0 that this edge bisects,
            // otherwise return site number 1
            return he.ELpm == Le ? he.ELedge.reg[Re] : he.ELedge.reg[Le];
        }



        /*********************************************************
         * Private methods - implementation details
         ********************************************************/



        private void Sort(double[] xValuesIn, double[] yValuesIn, int count)
        {
            _sites = null;
            _allEdges = new List<GraphEdge>();

            _nsites = count;
            _nvertices = 0;
            _nedges = 0;

            double sn = (double) _nsites + 4;
            _sqrtNsites = (int) Math.Sqrt(sn);

            // Copy the inputs so we don't modify the originals
            double[] xValues = new double[count];
            double[] yValues = new double[count];
            for (int i = 0; i < count; i++)
            {
                xValues[i] = xValuesIn[i];
                yValues[i] = yValuesIn[i];
            }

            SortNode(xValues, yValues, count);
        }



        private void SortNode(double[] xValues, double[] yValues, int numPoints)
        {
            _nsites = numPoints;
            _sites = new Site[_nsites];
            _xmin = xValues[0];
            _ymin = yValues[0];
            _xmax = xValues[0];
            _ymax = yValues[0];

            for (int i = 0; i < _nsites; i++)
            {
                _sites[i] = new Site();
                _sites[i].coord.setPoint(xValues[i], yValues[i]);
                _sites[i].sitenbr = i;

                if (xValues[i] < _xmin)
                    _xmin = xValues[i];
                else if (xValues[i] > _xmax)
                    _xmax = xValues[i];

                if (yValues[i] < _ymin)
                    _ymin = yValues[i];
                else if (yValues[i] > _ymax)
                    _ymax = yValues[i];
            }

            Qsort(_sites);
            _deltax = _xmax - _xmin;
            _deltay = _ymax - _ymin;
        }



        /*
         * implicit parameters: nsites, sqrt_nsites, xmin, xmax, ymin, ymax, deltax,
         * deltay (can all be estimates). Performance suffers if they are wrong;
         * better to make nsites, deltax, and deltay too big than too small. (?)
         */



        private bool voronoi_bd()
        {
            Site newsite, bot, top, temp, p;
            Site v;
            Point newintstar = null;
            int pm;
            Halfedge lbnd, rbnd, llbnd, rrbnd, bisector;
            Edge e;

            PQinitialize();
            ELinitialize();

            _bottomsite = Nextone();
            newsite = Nextone();
            while (true)
            {
                if (!PQempty()) newintstar = PQ_min();
                // if the lowest site has a smaller y value than the lowest vector
                // intersection,
                // process the site otherwise process the vector intersection

                if (newsite != null && (PQempty()
                                        || newsite.coord.y < newintstar.y
                                        || newsite.coord.y == newintstar.y
                                        && newsite.coord.x < newintstar.x))
                {
                    /* new site is smallest -this is a site event */
                    // get the first HalfEdge to the LEFT of the new site
                    lbnd = ELleftbnd(newsite.coord);
                    // get the first HalfEdge to the RIGHT of the new site
                    rbnd = ELright(lbnd);
                    // if this halfedge has no edge,bot =bottom site (whatever that
                    // is)
                    bot = Rightreg(lbnd);
                    // create a new edge that bisects
                    e = Bisect(bot, newsite);

                    // create a new HalfEdge, setting its ELpm field to 0
                    bisector = HEcreate(e, Le);
                    // insert this new bisector edge between the left and right
                    // vectors in a linked list
                    ELinsert(lbnd, bisector);

                    // if the new bisector intersects with the left edge,
                    // remove the left edge's vertex, and put in the new one
                    if ((p = intersect(lbnd, bisector)) != null)
                    {
                        PQdelete(lbnd);
                        PQinsert(lbnd, p, dist(p, newsite));
                    }

                    lbnd = bisector;
                    // create a new HalfEdge, setting its ELpm field to 1
                    bisector = HEcreate(e, Re);
                    // insert the new HE to the right of the original bisector
                    // earlier in the IF stmt
                    ELinsert(lbnd, bisector);

                    // if this new bisector intersects with the new HalfEdge
                    if ((p = intersect(bisector, rbnd)) != null)
                        // push the HE into the ordered linked list of vertices
                        PQinsert(bisector, p, dist(p, newsite));
                    newsite = Nextone();
                }
                else if (!PQempty())
                    /* intersection is smallest - this is a vector event */
                {
                    // pop the HalfEdge with the lowest vector off the ordered list
                    // of vectors
                    lbnd = PQextractmin();
                    // get the HalfEdge to the left of the above HE
                    llbnd = ELleft(lbnd);
                    // get the HalfEdge to the right of the above HE
                    rbnd = ELright(lbnd);
                    // get the HalfEdge to the right of the HE to the right of the
                    // lowest HE
                    rrbnd = ELright(rbnd);
                    // get the Site to the left of the left HE which it bisects
                    bot = Leftreg(lbnd);
                    // get the Site to the right of the right HE which it bisects
                    top = Rightreg(rbnd);

                    v = lbnd.vertex; // get the vertex that caused this event
                    makevertex(v); // set the vertex number - couldn't do this
                    // earlier since we didn't know when it would be processed
                    endpoint(lbnd.ELedge, lbnd.ELpm, v);
                    // set the endpoint of
                    // the left HalfEdge to be this vector
                    endpoint(rbnd.ELedge, rbnd.ELpm, v);
                    // set the endpoint of the right HalfEdge to
                    // be this vector
                    ELdelete(lbnd); // mark the lowest HE for
                    // deletion - can't delete yet because there might be pointers
                    // to it in Hash Map
                    PQdelete(rbnd);
                    // remove all vertex events to do with the right HE
                    ELdelete(rbnd); // mark the right HE for
                    // deletion - can't delete yet because there might be pointers
                    // to it in Hash Map
                    pm = Le; // set the pm variable to zero

                    if (bot.coord.y > top.coord.y)
                        // if the site to the left of the event is higher than the
                        // Site
                    {
                        // to the right of it, then swap them and set the 'pm'
                        // variable to 1
                        temp = bot;
                        bot = top;
                        top = temp;
                        pm = Re;
                    }

                    e = Bisect(bot, top); // create an Edge (or line)
                    // that is between the two Sites. This creates the formula of
                    // the line, and assigns a line number to it
                    bisector = HEcreate(e, pm); // create a HE from the Edge 'e',
                    // and make it point to that edge
                    // with its ELedge field
                    ELinsert(llbnd, bisector); // insert the new bisector to the
                    // right of the left HE
                    endpoint(e, Re - pm, v); // set one endpoint to the new edge
                    // to be the vector point 'v'.
                    // If the site to the left of this bisector is higher than the
                    // right Site, then this endpoint
                    // is put in position 0; otherwise in pos 1

                    // if left HE and the new bisector intersect, then delete
                    // the left HE, and reinsert it
                    if ((p = intersect(llbnd, bisector)) != null)
                    {
                        PQdelete(llbnd);
                        PQinsert(llbnd, p, dist(p, bot));
                    }

                    // if right HE and the new bisector intersect, then
                    // reinsert it
                    if ((p = intersect(bisector, rrbnd)) != null) PQinsert(bisector, p, dist(p, bot));
                }
                else
                {
                    break;
                }
            }

            for (lbnd = ELright(_eLleftend); lbnd != _eLrightend; lbnd = ELright(lbnd))
            {
                e = lbnd.ELedge;
                clip_line(e);
            }

            return true;
        }
    } // Voronoi Class End
} // namespace Voronoi2 End