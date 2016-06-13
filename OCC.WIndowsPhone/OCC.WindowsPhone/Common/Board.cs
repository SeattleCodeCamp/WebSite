#region License, Terms and Author(s)
//
// Orlando Code Camp for Windows Phone 7
// Copyright (c) 2012 Orlando .Net User Group. All rights reserved.
//
//  Author(s):
//
//      Brian Mishler, http://www.qualitydata.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System.Collections.Generic;
using OCC.WindowsPhone.Models;

namespace OCC.WindowsPhone.Common
{
    public class Board
    {
        public static List<BoardMember> GetBoardMembers()
        {
            return new List<BoardMember>
                       {
                            new BoardMember
                            {
                                Name="Esteban Garcia",
                                Title = "President",
                                Image = "Esteban.jpg"
                            },
                            new BoardMember
                            {
                                Name="John Smith",
                                Title = "Vice President",
                                Image = "JohnSmith.jpg"
                            },
                            new BoardMember
                            {
                                Name="John Torrey",
                                Title = "Treasurer",
                                Image = "johntorrey.jpg"
                            },
                            new BoardMember
                            {
                                Name="Brian Mishler",
                                Title = "Director of Marketing",
                                Image = "BrianMishler.jpg"
                            },
                            new BoardMember
                            {
                                Name="Brian Hall",
                                Title = "Director of Communications",
                                Image = "BrianHall.jpg"
                            },  
                            new BoardMember
                            {
                                Name="Zdravko Danev",
                                Title = "Director of Technology",
                                Image = "z.png"
                            }, 
                       };

        }
    }
}
